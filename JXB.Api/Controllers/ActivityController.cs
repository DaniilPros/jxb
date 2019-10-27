using JXB.Api.Data;
using JXB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JXB.Api.Services;

namespace JXB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMatchUsersService _service;

        public ActivityController(AppDbContext context,
            IMatchUsersService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet("GetByUser")]
        public ActivityVm GetUserActivity(string userId)
        {
            var dUsr = _context.DUsers.Include(item=>item.DActivity)
                .Include(item => item.DActivity.Activity)
                .Include(item => item.DActivity.Activity.Responsibilities)
                .FirstOrDefault(x => x.UserId == userId && x.DActivity.End != default);
            if (dUsr == null) return null;

            var users = dUsr.DActivity.DUsers
                .Select(dUser => new UserVm {UserId = dUser.UserId, Email = dUser.User.Email, Name = dUser.User.UserName}).ToList();

            return new ActivityVm
            {
                Id = dUsr.DActivity.Id,
                Name = dUsr.DActivity.Activity.Name,
                Responsibilities = dUsr.DActivity.Activity.Responsibilities.Select(x => x.Name),
                Time = dUsr.DActivity.Start,
                Users = users
            };
        }

        [HttpPost("CheckIn")]
        public async Task CheckIn([FromBody] CheckInRequest checkInRequest)
        {
            var dActivity = _context.DActivities.Include(item=>item.DUsers).FirstOrDefault(x => x.ActivityId == checkInRequest.ActivityId);
            var dUser = dActivity.DUsers.FirstOrDefault(x => x.UserId == checkInRequest.UserId);
            if (dUser == null) throw new NullReferenceException();

            dUser.CheckInTime = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();
        }

        [HttpPost("Rate")]
        public async Task Rate([FromBody] RateRequest rateRequest)
        {
            var dActivity = _context.DActivities.Include(item=>item.DUsers).FirstOrDefault(x => x.Id == rateRequest.DActivityId);
            if (dActivity == null) throw new NullReferenceException();

            dActivity.DUsers.FirstOrDefault(x => x.UserId == rateRequest.UserId).Rating = rateRequest.Rate;
            dActivity.End = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();
            await _service.CreateAvailableActivitiesAsync();
        }
    }
}
using JXB.Api.Data;
using JXB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using JXB.Api.Services;
using System.Collections.Generic;

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
            var dUsr = _context.DUsers.Include(item => item.DActivity)
                .ThenInclude(item => item.Activity)
                .ThenInclude(item => item.Responsibilities).Include(item => item.DActivity.DUsers)
                .FirstOrDefault(x => x.UserId == userId && x.DActivity.End == default);
            if (dUsr == null) return null;

            var userIds = dUsr.DActivity.DUsers
                .Select(x => x.UserId);

            var usrs = _context.Users.Where(x => userIds.Any(y => y == x.Id));
            var vms = new List<UserVm>();
            
            foreach (var u in usrs)
            {
                var usrVm = new UserVm { UserId = u.Id, Email = u.Email, Name = u.UserName };
                vms.Add(usrVm);
            }

            return new ActivityVm
            {
                Id = dUsr.DActivity.Id,
                Name = dUsr.DActivity.Activity.Name,
                Responsibilities = dUsr.DActivity.Activity.Responsibilities.Select(x => x.Name),
                Time = dUsr.DActivity.Start,
                Users = vms
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

            var dUser = dActivity.DUsers.FirstOrDefault(x => x.UserId == rateRequest.UserId);
            dUser.Rating = rateRequest.Rate;
            if (dActivity.DUsers.All(x => x.CheckInTime != default))dActivity.End = DateTimeOffset.UtcNow;

            _context.Update(dUser);
            await _context.SaveChangesAsync();
            await _service.CreateAvailableActivitiesAsync();
        }
    }
}
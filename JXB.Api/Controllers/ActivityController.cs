using JXB.Api.Data;
using JXB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JXB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActivityController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetByUser")]
        public ActivityVm GetUserActivity(string userId)
        {
            var dUsr = _context.DUsers.Include(item=>item.DActivity).Include(item => item.DActivity.Activity).Include(item => item.DActivity.Activity.Responsibilities).FirstOrDefault(x => x.UserId == userId && x.DActivity.End != default);
            if (dUsr == null) return null;

            var users = new List<UserVm>();
            foreach (var dUser in dUsr.DActivity.DUsers)
            {
                var user = new UserVm
                {
                    UserId = dUser.UserId,
                    Email = dUser.User.Email,
                    Name = dUser.User.UserName
                };

                users.Add(user);
            }

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
            var dActivity = _context.DActivities.Where(x => x.ActivityId == checkInRequest.ActivityId).FirstOrDefault();
            var dUser = dActivity.DUsers.FirstOrDefault(x => x.UserId == checkInRequest.UserId);
            if (dUser == null) throw new NullReferenceException();

            dUser.CheckInTime = DateTimeOffset.UtcNow;

            if (dActivity.DUsers.All(x => x.CheckInTime != default)) dActivity.End = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();
        }

        [HttpPost("Rate")]
        public async Task Rate([FromBody] RateRequest rateRequest)
        {
            var dActivity = _context.DActivities.Where(x => x.Id == rateRequest.DActivityId).FirstOrDefault();
            if (dActivity == null) throw new NullReferenceException();

            dActivity.DUsers.FirstOrDefault(x => x.UserId == rateRequest.UserId).Rating = rateRequest.Rate;

            await _context.SaveChangesAsync();
        }
    }
}
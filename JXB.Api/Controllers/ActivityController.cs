using JXB.Api.Data;
using JXB.Model;
using Microsoft.AspNetCore.Mvc;
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
            var dActivity = _context.DUsers.Where(x => x.UserId == userId && x.DActivity.End != default).FirstOrDefault().DActivity;

            var users = new List<UserVm>();
            foreach (var dUser in dActivity.DUsers)
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
                Id = dActivity.Id,
                Name = dActivity.Activity.Name,
                Responsibilities = dActivity.Activity.Responsibilities.Select(x => x.Name),
                Time = dActivity.Start,
                Users = users
            };
        }

        [HttpPost("CheckIn")]
        public async Task CheckIn([FromBody] CheckInRequest checkInRequest)
        {
            var dActivity = _context.DActivities.Where(x => x.ActivityId == checkInRequest.ActivityId).FirstOrDefault();
            dActivity.DUsers.FirstOrDefault(x => x.UserId == checkInRequest.UserId).CheckInTime = DateTimeOffset.UtcNow;

            if (dActivity.DUsers.All(x => x.CheckInTime != default)) dActivity.End = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();
        }

        [HttpPost("Rate")]
        public async Task Rate([FromBody] RateRequest rateRequest)
        {
            var dActivity = _context.DActivities.Where(x => x.Id == rateRequest.DActivityId).FirstOrDefault();
            dActivity.DUsers.FirstOrDefault(x => x.UserId == rateRequest.UserId).Rating = rateRequest.Rate;

            await _context.SaveChangesAsync();
        }
    }
}
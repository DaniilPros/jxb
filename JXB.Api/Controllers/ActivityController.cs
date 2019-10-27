using JXB.Api.Data;
using JXB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using JXB.Api.Services;
using System.Collections.Generic;
using JXB.Api.Notification;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace JXB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMatchUsersService _service;
        private readonly NotificationManager _notificationManager;

        public ActivityController(AppDbContext context,
            IMatchUsersService service,
            NotificationManager notificationManager)
        {
            _context = context;
            _service = service;
            _notificationManager = notificationManager;
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
            if (dActivity.DUsers.All(x => x.CheckInTime != default))
            {
                var userIds = dActivity.DUsers.Select(x => x.UserId).ToList();
                Task.Run(async () =>
                {
                    await Task.Delay(TimeSpan.FromMinutes(1));
                    var devices = _notificationManager.GetDevicesForTags(userIds);
                    foreach (var d in devices)
                    {
                        _notificationManager.SendMessage(d, _notificationManager.ConstructMessage("checkin"));
                    }

                });

            }

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


            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromMinutes(2));
                var userIds = await _service.CreateAvailableActivitiesAsync();
                var devices = _notificationManager.GetDevicesForTags(userIds);
                foreach (var d in devices)
                {
                    _notificationManager.SendMessage(d, _notificationManager.ConstructMessage("rate"));
                }
            });



        }
    }
}
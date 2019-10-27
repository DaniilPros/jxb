using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JXB.Api.Notification;
using JXB.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JXB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private NotificationManager notificationManager;

        public NotificationController(NotificationManager notificationManager)
        {
            this.notificationManager = notificationManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task Register([FromBody] Registration registration)
        {
            await notificationManager.Register(registration);
        }

        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest notificationRequest)
        {
            var devices = notificationManager.GetDevicesForTags(notificationRequest.Keys);

            if (devices.Count == 0)
            {
                return NotFound();
            }

            foreach (var device in devices)
            {
                await notificationManager.SendMessage(device,
                    notificationManager.ConstructMessage(notificationRequest.Message));
            }

            return Ok();
        }
    }
}
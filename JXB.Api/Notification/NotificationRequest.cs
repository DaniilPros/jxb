using System.Collections.Generic;

namespace JXB.Api.Notification
{
    public class NotificationRequest
    {
        public List<string> Keys { get; set; }
        public string Message { get; set; }
    }
}

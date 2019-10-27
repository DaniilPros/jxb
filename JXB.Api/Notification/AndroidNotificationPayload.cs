namespace JXB.Api.Notification
{
    public class AndroidNotificationPayload
    {
        public Notification notification { get; set; }
        public Data data { get; set; }
        public string collapseKey { get; set; }

        public class Notification
        {
            public string body_loc_key { get; set; }
            public string title_loc_key { get; set; }
        }

        public class Data
        {
            public string action { get; set; }
            public string data { get; set; }
        }
    }
}
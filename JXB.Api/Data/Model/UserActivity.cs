namespace JXB.Api.Data.Model
{
    public class UserActivity
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}

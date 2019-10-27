namespace JXB.Api.Data.Model
{
    public class UserActivity : BaseDataModel
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string ActivityId { get; set; }
        public Activity Activity { get; set; }
        public float Probability { get; set; }
    }
}

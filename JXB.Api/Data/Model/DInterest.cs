namespace JXB.Api.Data.Model
{
    public enum Answer
    {
        Default,
        First,
        Second
    }

    public class DInterest
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string InterestId { get; set; }
        public Interest Interest { get; set; }
        public int Result { get; set; }
    }
}
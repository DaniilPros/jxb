using JXB.Model;

namespace JXB.Api.Data.Model
{
    public class DQuestion : BaseDataModel
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string QuestionId { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
        public int Result { get; set; }
    }
}
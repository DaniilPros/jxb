using System.Collections.Generic;

namespace JXB.Model
{
    public class AnswerRequest
    {
        public string UserId { get; set; }
        public IDictionary<string, Answer> Answers { get; set; }
    }
}

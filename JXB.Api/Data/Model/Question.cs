using System.Collections.Generic;

namespace JXB.Api.Data.Model
{
    public class Question
    {
        public string Id { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Label { get; set; }
        public IEnumerable<DQuestion> DQuestions { get; set; }
    }
}

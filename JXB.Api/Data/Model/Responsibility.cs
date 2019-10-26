using System.Collections.Generic;

namespace JXB.Api.Data.Model
{
    public class Responsibility
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ActivityId { get; set; }
        public Activity Activity { get; set; }
        public IEnumerable<DUser> DUsers { get; set; }
    }
}
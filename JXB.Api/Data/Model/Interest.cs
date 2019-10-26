using System.Collections.Generic;

namespace JXB.Api.Data.Model
{
    public class Interest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<DInterest> DInterests { get; set; }
    }
}

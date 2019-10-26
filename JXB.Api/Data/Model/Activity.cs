using System.Collections.Generic;

namespace JXB.Api.Data.Model
{
    public class Activity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinUsersCount { get; set; }
        public int MaxUsersCount { get; set; }
        public float Rating { get; set; }
        public IEnumerable<Responsibility> Responsibilities { get; set; }
        public IEnumerable<DActivity> DActivities { get; set; }
    }
}

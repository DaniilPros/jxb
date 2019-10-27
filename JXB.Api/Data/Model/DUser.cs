using System;
using System.Collections.Generic;

namespace JXB.Api.Data.Model
{
    public class DUser : BaseDataModel
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string ResponsibilityId { get; set; }
        public Responsibility Responsibility { get; set; }
        public string DActivityId { get; set; }
        public DActivity DActivity { get; set; }

        public int Rating { get; set; }
        public DateTimeOffset? CheckInTime { get; set; }
    }
}
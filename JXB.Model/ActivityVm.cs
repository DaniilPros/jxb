using System;
using System.Collections;
using System.Collections.Generic;

namespace JXB.Model
{
    public class ActivityVm
    {
        public string Id { get; set; }
        public IEnumerable<UserVm> Users { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Time { get; set; }
        public IEnumerable<string> Responsibilities { get; set; }
    }
}

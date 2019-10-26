using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JXB.Api.Data.Model
{
    public class DActivity
    {
        public string Id { get; set; }
        public string ActivityId { get; set; }
        [Required]
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset? End { get; set; }
        public Activity Activity { get; set; }
        public IEnumerable<DUser> DUsers { get; set; }
    }
}
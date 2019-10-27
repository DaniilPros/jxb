using System.Collections.Generic;

namespace JXB.Api.Data.Model
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IEnumerable<DUser> DUsers { get; set; }
        public IEnumerable<UserActivity> UserActivities { get; set; }
        public IEnumerable<DQuestion> DQuestions { get; set; }
    }
}
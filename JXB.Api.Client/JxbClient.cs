using JXB.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JXB.Api.Client
{
    public class JxbClient
    {
        private Random _random = new Random();

        public JxbClient() { }

        public async Task<UserVm> Login(UserVm user)
        {
            var hasUser = _random.NextDouble() > 0.5;

            if (hasUser) return new UserVm
            {
                Email = "asd@asd.asd",
                Name = "Stepan",
                UserId = 1.ToString()
            };

            return null;
        }

        public async Task<IEnumerable<QuestionVm>> GetAllQuestions()
        {
            var result = new List<QuestionVm>();

            for (int i = 0; i < 10; i++)
            {
                result.Add(new QuestionVm
                {
                    Id = i.ToString(),
                    Option1 = $"Q{i} Option1",
                    Option2 = $"Q{i} Option2",
                });
            }

            return result;
        }

        public async Task SetQuestionResults(string userId, IDictionary<string, Answer> answers)
        {

        }

        public async Task<ActivityVm> GetScheduledActivityByUser(string userId)
        {
            var users = new List<UserVm>();

            for (int i = 0; i < 5; i++)
            {
                users.Add(new UserVm
                {
                    Email = $"asd@asd.asd{i}",
                    Name = $"Stepan{i}",
                    UserId = 1.ToString()
                });
            }

            return new ActivityVm
            {
                Id = 1.ToString(),
                Name = "Picnic",
                Time = DateTimeOffset.UtcNow,
                Responsibilities = new[] { "Resposibility 1", "Resposibility 2", "Resposibility 3" },
                Users = users
            };
        }

        public async Task CheckIn(string activityId)
        {

        }

        public async Task RateActivity(string activityId, int count)
        {

        }
    }
}

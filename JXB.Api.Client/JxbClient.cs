using JXB.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JXB.Api.Client
{
    public class JxbClient
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _url=
            string.Empty;

        public JxbClient(string url)
        {
            _url = url;
        }
        public JxbClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserVm> Login(LoginRequest loginRequest)
        {
            var content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url + $"api/User/Login", content);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserVm>(json);
        }

        public async Task<IEnumerable<QuestionVm>> GetAllQuestions()
        {
            var response = await _httpClient.GetStringAsync(_url + $"/api/Question/Get");
            return JsonConvert.DeserializeObject<IEnumerable<QuestionVm>>(response);
        }

        public async Task SetQuestionResults(AnswerRequest answers)
        {
            var content = new StringContent(JsonConvert.SerializeObject(answers), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_url + $"/api/Question/SetAnswers", content);
        }

        public async Task<ActivityVm> GetScheduledActivityByUser(string userId)
        {
            var response = await _httpClient.GetStringAsync(_url + $"/api/Activity/GetByUser?userId={userId}");
            return JsonConvert.DeserializeObject<ActivityVm>(response);
        }

        public async Task CheckIn(CheckInRequest checkInRequest)
        {
            var content = new StringContent(JsonConvert.SerializeObject(checkInRequest), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_url + $"/api/Activity/CheckIn", content);
        }

        public async Task RateActivity(RateRequest rateRequest)
        {
            var content = new StringContent(JsonConvert.SerializeObject(rateRequest), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_url + $"/api/Activity/Rate", content);
        }
    }
}

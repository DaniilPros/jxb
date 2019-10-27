using JXB.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JXB.Api.Client
{
    public class JxbClient
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _url;

        public JxbClient(string url)
        {
            _url = url;
        }

        public async Task<UserVm> Login(LoginRequest loginRequest)
        {
            var response = await _httpClient.GetStringAsync(_url + $"/api/User/Get?email={loginRequest.Email}&name={loginRequest.Name}");
            return JsonConvert.DeserializeObject<UserVm>(response);
        }

        public async Task<IEnumerable<QuestionVm>> GetAllQuestions()
        {
            var response = await _httpClient.GetStringAsync(_url + $"/api/Question/Get");
            return JsonConvert.DeserializeObject<IEnumerable<QuestionVm>>(response);
        }

        public async Task SetQuestionResults(AnswerRequest answers)
        {
            var content = new StringContent(JsonConvert.SerializeObject(answers));
            await _httpClient.PostAsync(_url + $"/api/Question/SetAnswers", content);
        }

        public async Task<ActivityVm> GetScheduledActivityByUser(string userId)
        {
            var response = await _httpClient.GetStringAsync(_url + $"/api/Activity/GetByUser?userId={userId}");
            return JsonConvert.DeserializeObject<ActivityVm>(response);
        }

        public async Task CheckIn(CheckInRequest checkInRequest)
        {
            var content = new StringContent(JsonConvert.SerializeObject(checkInRequest));
            await _httpClient.PostAsync(_url + $"/api/Activity/CheckIn", content);
        }

        public async Task RateActivity(RateRequest rateRequest)
        {
            var content = new StringContent(JsonConvert.SerializeObject(rateRequest));
            await _httpClient.PostAsync(_url + $"/api/Activity/Rate", content);
        }
    }
}

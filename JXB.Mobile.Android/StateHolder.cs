using JXB.Api.Client;
using JXB.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JXB
{
    public class StateHolder
    {
        public static StateHolder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StateHolder();
                }
                return instance;
            }
        }

        static StateHolder instance;

        public JxbClient Client { get; }

        public event Action OnRate;
        public event Action OnCheckIn;
        public event Action OnNew;

        public UserVm User { get; private set; }
        string settingsPath;
        string url = "https://jxbapi.azurewebsites.net";

        private StateHolder()
        {
            Client = new JxbClient(url);
        }

        public async Task ReadSettings(string path)
        {
            settingsPath = path;
            try
            {
                using (var settings = new FileStream(path, FileMode.Open))
                using (var streamReader = new StreamReader(settings))
                {
                    User = Newtonsoft.Json.JsonConvert.DeserializeObject<UserVm>(await streamReader.ReadToEndAsync());
                }
            }
            catch (Exception e)
            {

            }
        }

        async Task SaveSettings()
        {
            try
            {
                using (var settings = new FileStream(settingsPath, FileMode.Create))
                using (var streamWriter = new StreamWriter(settings))
                {
                    await streamWriter.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(User));
                }
            }
            catch (Exception e)
            {

            }
        }

        public async Task Login(string email)
        {
            try
            {
                User = await Client.Login(new LoginRequest
                {
                    Email = email,
                    Name = "Name"
                });
                await SaveSettings();
            }
            catch (Exception e)
            {

            }
        }

        public async Task<ActivityVm> GetActivity()
        {
            try
            {
                return await Client.GetScheduledActivityByUser(User.UserId);
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public async Task<IEnumerable<QuestionVm>> GetQuestions()
        {
            try
            {
                return await Client.GetAllQuestions();
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public async Task CheckIn(string id)
        {
            try
            {
                await Client.CheckIn( new CheckInRequest
                {
                    UserId = User.UserId,
                    ActivityId = id
                });
            }
            catch (Exception e)
            {

            }
        }

        public async Task SubmitAnswers(Dictionary<string, Answer> answers)
        {
            try
            {
                await Client.SetQuestionResults(new AnswerRequest
                {
                    UserId = User.UserId,
                    Answers = answers
                });
                User.IsNew = false;
                await SaveSettings();
            }
            catch (Exception e)
            {

            }
        }

        public void RegisterForNotifications(string token)
        {
            Task.Run(async () =>
            {
                IsRegistered = true;
                await Register(token);
            });
        }

        public void OnNotificationReceived(string data)
        {
            switch (data)
            {
                case "rate":
                    OnRate?.Invoke();
                    break;
                case "checkin":
                    OnCheckIn?.Invoke();
                    break;
                case "new":
                    OnNew?.Invoke();
                    break;
            }
        }

        static readonly string unregister = "/api/notification/unregister";
        static readonly string register = "/api/notification/register";

        public static bool IsRegistered { get; private set; }

        async Task Register(string id)
        {
            try
            {
                var builder = new UriBuilder(url + register);
                var client = new HttpClient();
                var registration = new Registration
                {
                    Keys = new List<string> { User.UserId },
                    Handle = id
                };

                var s = Newtonsoft.Json.JsonConvert.SerializeObject(registration);
                var message = new HttpRequestMessage
                {
                    RequestUri = builder.Uri,
                    Method = HttpMethod.Post,
                    Content = new StringContent(s)
                };
                message.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var res = await client.SendAsync(message);
                if (res.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    IsRegistered = false;
                }
            }
            catch (Exception e)
            {
            }
        }

        class Registration
        {
            public List<string> Keys { get; set; }
            public string Handle { get; set; }
        }
    }
}

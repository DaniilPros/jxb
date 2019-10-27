using JXB.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JXB.Api.Client.Test
{
    public class Tests
    {
        private JxbClient client;

        public TestServer Server { get; } = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
        [SetUp]
        public void Setup()
        {
            client = new JxbClient(Server.CreateClient());
        }

        //[Test]
        //public async Task Login()
        //{
        //    var loginRequest = new LoginRequest { Email = "iliaskirko@gmail.com", Name = "iliaskirko" };
        //    var user = await client.Login(loginRequest);

        //    //do some testing here
        //    Assert.Pass();
        //}

        //[Test]
        //public async Task LoginNew()
        //{
        //    var loginRequest = new LoginRequest { Email = "asdadddd", Name = "asdaaa" };
        //    var user = await client.Login(loginRequest);

        //    //do some testing here
        //    Assert.Pass();
        //}

        //[Test]
        //public async Task GetQuestions()
        //{
        //    var questions = await client.GetAllQuestions();

        //    //do some testing here
        //    Assert.Pass();
        //}

        //[Test]
        //public async Task SetAnswers()
        //{
        //    var answers = new Dictionary<string, Answer> 
        //    {
        //        {"7e40d914-87cb-40af-882a-7768d4e16e91", Answer.Option1 },
        //        {"7e40d914-87cb-40af-882a-7768d4e16e92", Answer.Option2 }
        //    };

        //    var answerRequest = new AnswerRequest
        //    {
        //        UserId = "505943f9-4e28-4ec0-b4b0-6818a59a6085",
        //        Answers = answers
        //    };

        //    await client.SetQuestionResults(answerRequest);

        //    //do some testing here
        //    Assert.Pass();
        //}

        //[Test]
        //public async Task GetActivity()
        //{
        //    var activity = await client.GetScheduledActivityByUser("53d4b3d9-57f1-49c0-a907-b27c6e14aa62");

        //    //do some testing here
        //    Assert.Pass();
        //}

        //[Test]
        //public async Task CheckIn()
        //{
        //    var checkinRequest = new CheckInRequest
        //    {
        //        ActivityId = "",
        //        UserId = "505943f9-4e28-4ec0-b4b0-6818a59a6085"
        //    };
        //    await client.CheckIn(checkinRequest);

        //    //do some testing here
        //    Assert.Pass();
        //}

        [Test]
        public async Task Rate()
        {
            var rateRequest = new RateRequest
            {
                UserId = "505943f9-4e28-4ec0-b4b0-6818a59a6085",
                DActivityId = "", 
                Rate = 5,
            };
            await client.RateActivity(rateRequest);

            //do some testing here
            Assert.Pass();
        }
    }

}
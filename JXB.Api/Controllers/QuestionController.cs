using System.Collections.Generic;
using System.Threading.Tasks;
using JXB.Api.Data;
using JXB.Api.Data.Model;
using JXB.Model;
using Microsoft.AspNetCore.Mvc;

namespace JXB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuestionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Get")]
        public IEnumerable<QuestionVm> GetQuestions()
        {
            var questions = _context.Questions;

            var result = new List<QuestionVm>();

            foreach (var question in questions)
            {
                result.Add(new QuestionVm
                {
                    Id = question.Id,
                    Option1 = question.Option1,
                    Option2 = question.Option2
                });
            }

            return result;
        }

        [HttpPost("SetAnswers")]
        public async Task SetAnswers([FromBody] AnswerRequest request)
        {
            foreach (var answer in request.Answers)
            {
                var dQuestion = new DQuestion
                {
                    UserId = request.UserId,
                    QuestionId = answer.Key,
                    Answer = answer.Value
                };

                await _context.AddAsync(dQuestion);
            }
        }
    }
}
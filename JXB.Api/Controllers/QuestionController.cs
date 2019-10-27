﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JXB.Api.Data;
using JXB.Api.Data.Model;
using JXB.Api.Services;
using JXB.Model;
using Microsoft.AspNetCore.Mvc;

namespace JXB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IActivityPredictionService _predictionService;
        private readonly IMatchUsersService _matchUsersService;
        private static Random _random = new Random();

        public QuestionController(AppDbContext context,
            IActivityPredictionService predictionService,
            IMatchUsersService matchUsersService)
        {
            _context = context;
            _predictionService = predictionService;
            _matchUsersService = matchUsersService;
        }

        [HttpGet("Get")]
        public IEnumerable<QuestionVm> GetQuestions()
        {
            var questions = _context.Questions;
            questions.OrderBy(x => _random.Next()).Take(7);

            return questions.Select(question => new QuestionVm {Id = question.Id, Option1 = question.Option1, Option2 = question.Option2}).ToList();
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

                _context.DQuestions.Add(dQuestion);
            }

            await _context.SaveChangesAsync();
            await _predictionService.CreateActivityPredictionsAsync(request.UserId);
            await _matchUsersService.CreateAvailableActivitiesAsync();
        }
    }
}
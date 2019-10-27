using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JXB.Api.Data;
using JXB.Api.Data.Model;
using JXB.BackendML.Model;
using Microsoft.ML.Data;

namespace JXB.Api.Services
{
    public class ActivityPredictionService:IActivityPredictionService
    {
        private readonly ConsumeModel _model;
        private readonly AppDbContext _dbContext;

        public ActivityPredictionService(ConsumeModel model,AppDbContext dbContext)
        {
            _model = model;
            _dbContext = dbContext;
        }

        public async Task CreateActivityPredictionsAsync(string userId)
        {
            var result = GetActivityProbabilities(userId);
            foreach (var userActivity in result.Keys.Select(resultKey => new UserActivity
            {
                Id = Guid.NewGuid().ToString("D"),
                UserId = userId,
                ActivityId = _dbContext.Activities.First(item => item.Label == resultKey).Id,
                Probability = result[resultKey]
            }))
            {
                _dbContext.UserActivities.Add(userActivity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Dictionary<string, float> GetActivityProbabilities(string userId)
        {
            var modelInput = new ModelInput
            {
                Col1 = GetValue(userId, nameof(ModelInput.Col1)),
                Col2 = GetValue(userId, nameof(ModelInput.Col2)),
                Col3 = GetValue(userId, nameof(ModelInput.Col3)),
                Col4 = GetValue(userId, nameof(ModelInput.Col4)),
                Col5 = GetValue(userId, nameof(ModelInput.Col5)),
                Col6 = GetValue(userId, nameof(ModelInput.Col6)),
                Col7 = GetValue(userId, nameof(ModelInput.Col7)),
                Col8 = GetValue(userId, nameof(ModelInput.Col8)),
                Col9 = GetValue(userId, nameof(ModelInput.Col9)),
                Col10 = GetValue(userId, nameof(ModelInput.Col10)),
                Col11 = GetValue(userId, nameof(ModelInput.Col11)),
                Col12 = GetValue(userId, nameof(ModelInput.Col12)),
                Col13 = GetValue(userId, nameof(ModelInput.Col13)),
                Col14 = GetValue(userId, nameof(ModelInput.Col14)),
                Col15 = GetValue(userId, nameof(ModelInput.Col15)),
                Col16 = GetValue(userId, nameof(ModelInput.Col16)),
                Col17 = GetValue(userId, nameof(ModelInput.Col17))
            };

            var output = _model.Predict(modelInput);

            var result = new Dictionary<string,float>();

            for (var i = 0; i < output.Score.Length; i++)
                result.Add($"{i + 1}", output.Score[i]);

            return result;
        }

        private float GetValue(string userId,string propertyName)
        {
            var propertyInfo = typeof(ModelInput).GetProperty(propertyName);
            if (propertyInfo?.GetCustomAttributes(typeof(LabelAttribute), false).FirstOrDefault() is LabelAttribute attribute)
            {
                //get date from dbContext
                return 0;
            }

            return 0;
        }
    }
}
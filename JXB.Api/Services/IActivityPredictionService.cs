using System.Collections.Generic;

namespace JXB.Api.Services
{
    public interface IActivityPredictionService
    {
        Dictionary<string, float> GetActivityProbabilities(string userId);
    }
}
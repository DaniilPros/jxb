using System.Collections.Generic;
using System.Threading.Tasks;

namespace JXB.Api.Services
{
    public interface IActivityPredictionService
    {
        Task CreateActivityPredictionsAsync(string userId);
    }
}
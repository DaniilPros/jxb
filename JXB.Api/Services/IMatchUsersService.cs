using System.Collections.Generic;
using System.Threading.Tasks;

namespace JXB.Api.Services
{
    public interface IMatchUsersService
    {
        Task<List<string>> CreateAvailableActivitiesAsync();

    }
}
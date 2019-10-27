using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JXB.Api.Data;
using JXB.Api.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace JXB.Api.Services
{
    public class MatchUsersService : IMatchUsersService
    {
        private readonly AppDbContext _context;

        public MatchUsersService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAvailableActivitiesAsync()
        {
            var activities = await _context.Activities.ToListAsync();

            foreach (var activity in activities)
            {
                var users = await GetAvailableUsers();

                var userActivities = await _context.UserActivities
                    .Where(item => item.ActivityId == activity.Id && users.Any(it => item.UserId == it.Id))
                    .OrderBy(item=>item.Probability)
                    .Take(2)//can be another number
                    .ToListAsync();

                if (userActivities.Count >= activity.MinUsersCount)
                {
                    var count = Math.Min(userActivities.Count, activity.MaxUsersCount);
                    foreach (var userActivity in userActivities.Take(count))
                    {
                        var dActivity = new DActivity
                        {
                            ActivityId = userActivity.ActivityId, Start = DateTimeOffset.UtcNow
                        };
                        await _context.DActivities.AddAsync(dActivity);
                        await _context.SaveChangesAsync();

                        var dUser = new DUser {UserId = userActivity.UserId, DActivityId = dActivity.ActivityId};

                        await _context.DUsers.AddAsync(dUser);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        private async Task<List<User>> GetAvailableUsers()
        {
            return await _context.DUsers.Include(item => item.User)
                .Include(item => item.DActivity)
                .Where(item => item.DActivity.End == null)
                .Select(item => item.User)
                .Distinct()
                .ToListAsync();
        }
    }
}
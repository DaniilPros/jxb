using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JXB.Api.Data;
using JXB.Api.Data.Model;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
                var users = GetAvailableUsers();

                var selectedUsers = users
                    .Where(user => user.UserActivities.Where(item => item.ActivityId == activity.Id && item.UserId == user.Id)
                        .OrderByDescending(item => item.Probability)
                        .Take(2)
                        .Any(item => item.ActivityId == activity.Id))
                    .ToList();

                if (selectedUsers.Count >= activity.MinUsersCount)
                {
                    
                    var count = Math.Min(selectedUsers.Count, activity.MaxUsersCount);
                    foreach (var selectedUser in selectedUsers.Take(count))
                    {
                        var dActivity = new DActivity
                        {
                            ActivityId = activity.Id, Start = DateTimeOffset.UtcNow
                        };
                        await _context.DActivities.AddAsync(dActivity);
                        await _context.SaveChangesAsync();

                        var dUser = new DUser {UserId = selectedUser.Id, DActivityId = dActivity.ActivityId};

                        await _context.DUsers.AddAsync(dUser);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        private IEnumerable<User> GetAvailableUsers()
        {
            return _context.DUsers.Include(item => item.User)
                .Include(item => item.User.UserActivities)
                .Include(item => item.DActivity)
                .Where(item => item.DActivity.End == null)
                .ToList()
                .Select(item => item.User)
                .Distinct();
        }
    }
}
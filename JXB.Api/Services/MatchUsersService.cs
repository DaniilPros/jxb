using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
                    var dActivity = new DActivity
                    {
                        ActivityId = activity.Id,
                        Start = DateTimeOffset.UtcNow
                    };
                    await _context.DActivities.AddAsync(dActivity);
                    await _context.SaveChangesAsync();

                    var count = Math.Min(selectedUsers.Count, activity.MaxUsersCount);
                    foreach (var selectedUser in selectedUsers.Take(count))
                    {
                        var dUser = new DUser {UserId = selectedUser.Id, DActivityId = dActivity.Id};

                        await _context.DUsers.AddAsync(dUser);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        private IEnumerable<User> GetAvailableUsers()
        {
            return _context.Users.Include(item => item.DUsers)
                .Include(item => item.UserActivities)
                .Where(user => !user.DUsers.Any() ||
                _context.DUsers.Include(item => item.DActivity)
                .Where(item=>item.UserId==user.Id)
                .All(item => item.DActivity != null && item.DActivity.End != null));
                //.ToList()
                //.Union(_context.DUsers.Include(item => item.User)
                //.Include(item => item.User.UserActivities)
                //.Include(item => item.DActivity)
                //.Where(item => item.DActivity!=null&&item.DActivity.End != null)
                //.ToList()
                //.Select(item => item.User)
                //.Distinct(new Comparer()));

        }
    }
    public class Comparer : IEqualityComparer<User>
    {
        public bool Equals([AllowNull] User x, [AllowNull] User y)
        {
            return x?.Id == y?.Id;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.GetHashCode();
        }
    }
}
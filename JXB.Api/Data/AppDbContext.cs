using JXB.Api.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace JXB.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<DUser> DUsers { get; set; }
        public DbSet<DQuestion> DQuestions { get; set; }
        public DbSet<DActivity> DActivities { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }
    }
}
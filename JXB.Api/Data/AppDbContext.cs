using JXB.Api.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace JXB.Api.Data
{
    public class AppDbContext:DbContext
    {
       public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
       {
       }

       public DbSet<User> Users { get; set; }
    }
}
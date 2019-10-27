using System.Linq;
using System.Threading.Tasks;
using JXB.Api.Data;
using JXB.Api.Data.Model;
using JXB.Model;
using Microsoft.AspNetCore.Mvc;

namespace JXB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Get")]
        public async Task<UserVm> GetUser(LoginRequest loginRequest)
        {
            var user = _context.Users.Where(x => x.Email == loginRequest.Email).FirstOrDefault();
            if (user == null) user = await AddUser(loginRequest);

            return new UserVm
            {
                UserId = user.Id,
                Email = user.Email,
                Name = user.UserName
            };
        }

        public async Task<User> AddUser(LoginRequest loginRequest)
        {
            var user = new User
            {
                Email = loginRequest.Email,
                UserName = loginRequest.Name
            };

            _context.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
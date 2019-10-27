using System;
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

        [HttpPost("Login")]
        public async Task<UserVm> GetUser([FromBody]LoginRequest loginRequest)
        {
            var isNew = false;
            var user = _context.Users.FirstOrDefault(x => x.Email == loginRequest.Email);

            if (user == null)
            {
                isNew = true;
                user = await AddUser(loginRequest);
            }

            return new UserVm
            {
                UserId = user.Id,
                Email = user.Email,
                Name = user.UserName,
                IsNew = isNew
            };
        }

        public async Task<User> AddUser(LoginRequest loginRequest)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = loginRequest.Email,
                UserName = loginRequest.Name
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
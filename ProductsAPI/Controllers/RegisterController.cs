using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        private readonly UsersDbContext dbContext;

        public RegisterController(UsersDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(AddUserRequest adduser)
        {
            var user = new User() 
            {
                Id = Guid.NewGuid(),
                Username = adduser.Username,
                Password = adduser.Password,
                Role="user"
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await dbContext.Users.ToListAsync());
        }
    }
}

using ApiWithAuthentication.Attributes;
using ApiWithAuthentication.Data;
using ApiWithAuthentication.Models.ViewModels;
using ApiWithAuthentication.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWithAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly AppDbContext _dbContext;

        public AuthenticationController(ITokenService tokenService, AppDbContext dbContext)
        {
            _tokenService = tokenService;
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Token([FromBody] UserLoginViewModel user)
        {
            var allUsers = await _dbContext.Users.ToListAsync();
            var userFromDatabase = await _dbContext.Users
                .AsNoTracking()
                .Include(_ => _.Role)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (userFromDatabase is null)
            {
                return BadRequest("User not found");
            }

            if (!Models.User.IsValidPassword(userFromDatabase.PasswordHash, user.Password))
            {
                return BadRequest("Invalid password");
            }
            var token = _tokenService.GenerateToken(userFromDatabase);
            return Ok(new
            {
                User = userFromDatabase,
                AccessToken = token
            });
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("only-user-can-access")]
        public IActionResult UserRouteClaim()
        {
            return Ok("You are User loggedIn!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("only-admin-can-access")]
        public IActionResult AdminRouteClaim()
        {
            return Ok("You are Admin loggedIn!");
        }

        [ApiKey]
        [HttpGet]
        [Route("only-api-key-can-access")]
        public IActionResult ApiKeyRoute()
        {
            return Ok("You are using Api Key!");
        }
    }
}

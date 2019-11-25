using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_service.Models;
using web_service.Repositories;
using web_service.Services.Auth;

namespace web_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly UserRepository repository;

        public HomeController(UserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Authenticate(Login login)
        {
            var user = await repository.GetUserByUsername(login.Username);
            
            if (AuthManager.AuthenticateUser(user, login.Password))
            {
                var token = JwtToken.GenerateToken(user);
                user.Password = "";

                return Ok(new
                {
                    user = user,
                    token = token
                });
            }

            return BadRequest(new { message = "Username ou password inválidos !" });
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace web_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        /*
        private readonly UsuarioRepository repository;

        public HomeController(UsuarioRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Authenticate(Login login)
        {
            var usuario = await repository.GetUsuarioByUsername(login.Username);

            if (usuario != null && 
                PasswordManager.CheckHashPassword(login.Password, usuario.Password))
            {
                var token = JwtToken.GenerateToken(usuario);
                usuario.Password = "";

                return Ok(new
                {
                    user = usuario,
                    token = token
                });
            }

            return BadRequest(new { message = "Username ou password inválidos !" });
        }
        */
    }
}
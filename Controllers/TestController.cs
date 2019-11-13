using Microsoft.AspNetCore.Mvc;
using web_service.database;
using System.Threading.Tasks;
using web_service.Models;


namespace web_service.Controllers
{   
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly WebServiceContext context;
        public TestController(WebServiceContext context)
        {
            this.context = context;
        }
        
        [HttpPost("usuario")]
        public async Task<ActionResult> Create([FromBody]Usuario usuario)
        {   
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetUsuario", new {id = usuario.Id}, usuario);
        }

        [HttpGet("usuario/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);

            if (usuario != null) {
                return usuario;
            }

            return NotFound();
        }
    }
}
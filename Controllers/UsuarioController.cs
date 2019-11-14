using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_service.Models;
using web_service.Repositories;

namespace web_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository repository;

        public UsuarioController(UsuarioRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await repository.FindUsuarioAsync(id);

            if (usuario != null)
                return usuario;

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuarios = await repository.GetUsuariosAsync();

            if (usuarios != null)
                return usuarios;

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Usuario usuario)
        {
            if (await repository.CreateUsuarioAsync(usuario))
                return RedirectToAction("Get", new { id = usuario.Id });

            return StatusCode(500, new
            {
                message = "Não foi possível registrar o usuário."
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Update(int id, Usuario u)
        {
            if (await repository.UpdateUsuarioAsync(id, u))
                return NoContent();

            return StatusCode(500, new
            {
                message = "Não foi possível atualiza o usuário"
            });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            if (await repository.DeleteUsuarioAsync(id))
            {
                return Ok(new
                {
                    message = "Usuario excluído com sucesso !",
                });
            }

            return NotFound();
        }

    }
}
using System;
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
            try
            {
                await repository.CreateUsuarioAsync(usuario);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return RedirectToAction("Get", new { id = usuario.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Update(int id, Usuario usuario)
        {
            try
            {
                if (await repository.UpdateUsuarioAsync(id, usuario))
                    return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return NotFound(new { message = "Usuário não encontrado !" });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            try
            {
                if (await repository.DeleteUsuarioAsync(id))
                    return Ok(new { message = "Usuario excluído com sucesso !" });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }


            return NotFound(new { message = "Usuário não encontrado !" });
        }

    }
}
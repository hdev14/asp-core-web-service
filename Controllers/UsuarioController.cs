using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_service.database;
using web_service.Models;

namespace web_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        
        private readonly WebServiceContext context;
        
        public UsuarioController(WebServiceContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {   
            var usuario = await context.Usuarios.FindAsync(id);
            
            if (usuario != null)
                return usuario;

            return NotFound();
        }

        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuarios = await context.Usuarios.ToListAsync();

            if (usuarios != null)
                return usuarios;

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(Usuario usuario)
        {   
            bool result = await this.RegistrarUsuario(usuario);
            
            if (result)
                return CreatedAtAction("Get", new {id = usuario.Id}, usuario);
    
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Update(int id, Usuario usuario)
        {
            var u = await context.Usuarios.FindAsync(id);
            if (u != null)
            {
                u.Nome = usuario.Nome;
                u.Username = usuario.Username;

                if (await this.SalvarDados())
                    return CreatedAtAction("Get", new { id = u.Id}, u);
            }

            return NotFound();
        }
        

        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);
            
            if (usuario == null) 
                return NotFound();

            context.Usuarios.Remove(usuario);

            if (await this.SalvarDados())
                return Ok();

            return NotFound();
        }

        private async Task<bool> RegistrarUsuario(Usuario usuario)
        {   
            
            try
            {
                context.Usuarios.Add(usuario);
                if (await this.SalvarDados())
                    return true;
                
            } 
            catch (DbUpdateException e)
            {
                throw;
            }
            
            return false;
        }

        private async Task<bool> SalvarDados()
        {
            return await context.SaveChangesAsync() != 0;
        }

    }
}
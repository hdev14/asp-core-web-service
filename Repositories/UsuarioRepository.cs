using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_service.database;
using web_service.Models;

namespace web_service.Repositories
{
    public class UsuarioRepository
    {
        private readonly WebServiceContext context;

        public UsuarioRepository(WebServiceContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateUsuarioAsync(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            return (await context.SaveChangesAsync() != 0);
        }

        public async Task<bool> UpdateUsuarioAsync(int id, Usuario u)
        {

            var usuario = await this.FindUsuarioAsync(id);

            if (usuario != null)
            {
                usuario.Nome = u.Nome;
                usuario.Username = u.Username;

                return (await context.SaveChangesAsync() != 0);
            }

            return false;
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await this.FindUsuarioAsync(id);

            if (usuario != null)
            {
                context.Usuarios.Remove(usuario);
                return (await context.SaveChangesAsync() != 0);
            }

            return false;
        }

        public async Task<Usuario> FindUsuarioAsync(int id)
        {
            return await context.Usuarios.FindAsync(id);
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await context.Usuarios.ToListAsync();
        }

    }
}
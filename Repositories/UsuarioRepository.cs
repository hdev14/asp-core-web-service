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

        public async Task<bool> UpdateUsuarioAsync(int id, Usuario u)
        {

            var usuario = await context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                usuario.Nome = u.Nome;
                usuario.Username = u.Username;
                if (await this.SalvarDados())
                    return true;
            }

            return false;
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await this.FindUsuarioAsync(id);

            if (usuario != null)
            {
                context.Usuarios.Remove(usuario);
                if (await this.SalvarDados())
                    return true;
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

        protected async Task<bool> SalvarDados()
        {
            return await context.SaveChangesAsync() != 0;
        }

    }
}
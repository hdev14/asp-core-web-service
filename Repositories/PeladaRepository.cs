using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_service.database;
using web_service.Models;

namespace web_service.Repositories
{
    public class PeladaRepository
    {   
        private readonly WebServiceContext context;

        public PeladaRepository(WebServiceContext context)
        {
            this.context = context;
        }

        public async Task CreatePeladaAsync(Pelada pelada)
        {
            context.Peladas.Add(pelada);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePeladaAsync(int id, Pelada p)
        {
            var pelada = await this.FindPeladaAsync(id);
            if (pelada != null)
            {
                pelada.Titulo = p.Titulo;
                pelada.Descricao = p.Descricao;
                pelada.Local = p.Local;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeletePeladaAsync(int id)
        {
            var pelada = await this.FindPeladaAsync(id);
            if (pelada != null)
            {
                context.Peladas.Remove(pelada);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Pelada> FindPeladaAsync(int id)
        {
            return await context.Peladas.FindAsync(id);
        }

        public async Task<List<Pelada>> GetPeladasAsync()
        {
            return await context.Peladas.ToListAsync();
        }
    }
}
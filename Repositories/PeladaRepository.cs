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

        public async Task<Pelada> FindPeladaAsync(int id)
        {
            return await context.Pelada.FindAsync(id);
        }

        public async Task<List<Pelada>> FindPeladasAsync()
        {
            return await context.Pelada.ToListAsync();
        }

        public async Task CreatePeladaAsync(Pelada p)
        {
            context.Pelada.Add(p);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePeladaAsync(int id, Pelada p)
        {
            var pelada = await this.FindPeladaAsync(id);
            if (pelada != null)
            {
                pelada.Title = p.Title;
                pelada.Description = p.Description;
                pelada.Place = p.Place;
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
                context.Pelada.Remove(pelada);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_service.database;
using web_service.Models;

namespace web_service.Repositories
{
    public class AtletaRepository
    {

        private readonly WebServiceContext context;
        
        public AtletaRepository(WebServiceContext context)
        {   
            this.context = context;
        }

        public async Task CreateAtletaAsync(Atleta atleta)
        {
            context.Atletas.Add(atleta);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAtletaAsync(int id, Atleta a)
        {
            var atleta = await this.FindAtletaAsync(id);
            if (atleta != null)
            {
                atleta.Nome = a.Nome;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAtletaAsync(int id)
        {
            var atleta = await this.FindAtletaAsync(id);
            if (atleta != null)
            {
                context.Atletas.Remove(atleta);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Atleta> FindAtletaAsync(int id)
        {
            return await context.Atletas.FindAsync(id);
        }

        public async Task<List<Atleta>> GetAtletasAsync()
        {
            return await context.Atletas.ToListAsync();
        }
    }
}
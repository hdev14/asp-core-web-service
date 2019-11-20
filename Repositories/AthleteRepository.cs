using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_service.database;
using web_service.Models;

namespace web_service.Repositories
{
    public class AthleteRepository
    {
        
        /*
        private readonly WebServiceContext context;
        
        public AthleteRepository(WebServiceContext context)
        {   
            this.context = context;
        }

        public async Task CreateModelAsync(Athlete a)
        {
            context.Atletas.Add(a);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateModelAsync(int id, Athlete a)
        {   
            var atleta = await this.FindModelAsync(id);
            if (atleta != null)
            {
                atleta.Nome = a.Nome;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteModelAsync(int id)
        {
            var atleta = (Atleta) await this.FindModelAsync(id);
            if (atleta != null)
            {
                context.Atletas.Remove(atleta);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Athlete> FindModelAsync(int id)
        {
            return await context.Atletas.FindAsync(id);
        }

        public async Task<List<Athlete>> FindModelsAsync()
        {
            return await context.Athlete.ToListAsync();
        }
        */
    }
}
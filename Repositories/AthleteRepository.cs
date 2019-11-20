using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_service.database;
using web_service.Models;

namespace web_service.Repositories
{
    public class AthleteRepository
    {
        private readonly WebServiceContext context;
        
        public AthleteRepository(WebServiceContext context)
        {   
            this.context = context;
        }

        public async Task CreateAthleteAsync(Athlete a)
        {
            context.Athlete.Add(a);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAthleteAsync(int id, Athlete a)
        {   
            var athlete = await this.FindAthleteAsync(id);
            if (athlete != null)
            {
                athlete.Name = a.Name;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAthleteAsync(int id)
        {
            var athlete = await this.FindAthleteAsync(id);
            if (athlete != null)
            {
                context.Athlete.Remove(athlete);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Athlete> FindAthleteAsync(int id)
        {
            return await context.Athlete.FindAsync(id);
        }

        public async Task<List<Athlete>> FindAthletesAsync()
        {
            return await context.Athlete.ToListAsync();
        }
    }
}
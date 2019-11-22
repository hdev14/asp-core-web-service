using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_service.database;
using web_service.Models;


namespace web_service.Repositories
{
    public class SportRepository
    {
        private readonly WebServiceContext context;

        public SportRepository(WebServiceContext context)
        {
            this.context = context;
        }

        public async Task CreateSportAsync(Sport s)
        {
            context.Sport.Add(s);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateSportAsync(int id, Sport s)
        {
            var sport = await this.FindSportAsync(id);

            if (sport != null)
            {
                sport.Name = s.Name;
                sport.NumberPlayers = s.NumberPlayers;
                sport.NumberPlayersTeam = s.NumberPlayersTeam;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteSportAsync(int id)
        {
            var sport = await this.FindSportAsync(id);

            if (sport != null)
            {
                context.Sport.Remove(sport);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Sport> FindSportAsync(int id)
        {
            return await context.Sport.Include(s => s.Pelada).Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Sport>> FindSportsAsync()
        {
            return await context.Sport.ToListAsync();
        }
    }
}
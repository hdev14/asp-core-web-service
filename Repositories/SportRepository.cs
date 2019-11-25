using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_service.database;
using web_service.Models;
using web_service.ModelsView;

namespace web_service.Repositories
{
    public class SportRepository
    {
        private readonly WebServiceContext context;
        public bool IsReserve { get; set; }

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
            return await context.Sport.Where(s => s.Id == id)
                            .Include(s => s.Peladas)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<SportView>> FindSportsAsync()
        {
            return await context.Sport
                                .Select(sport => new SportView
                                {
                                    Id = sport.Id,
                                    Name = sport.Name,
                                    NumberPlayers = sport.NumberPlayers,
                                    NumberPlayersTeam = sport.NumberPlayersTeam
                                })
                                .ToListAsync();
        }
        

        public string[] getArrayQuantityTeams(Sport sport, int numberAthletes)
        {
            double quantity = this.getQuantityOfTeams(numberAthletes, sport.NumberPlayersTeam);
            string quantityInString = quantity.ToString();

            return quantityInString.Split(',', System.StringSplitOptions.None);
        }

        public void CheckReserve(int numberAfterComma)
        {
            this.IsReserve = (numberAfterComma != 0);
        }

        private double getQuantityOfTeams(int numberAthletes, int numberPlayers)
        {
            return numberAthletes / numberPlayers;
        }
    }
}
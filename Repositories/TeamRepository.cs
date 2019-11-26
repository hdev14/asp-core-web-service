using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_service.database;
using web_service.Models;
using web_service.ModelsView;

namespace web_service.Repositories
{
    public class TeamRepository
    {
        private readonly WebServiceContext context;
        public bool IsReserve { get; set; }

        public TeamRepository(WebServiceContext context)
        {
            this.context = context;
            this.IsReserve = false;
        }

        public async Task CreateTeamAsync(Team t)
        {
            context.Team.Add(t);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTeamAsync(int id, Team t)
        {
            var team = await this.FindTeamAsync(id);
            if (team != null)
            {
                team.Name = t.Name;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteTeamAsync(int id)
        {
            var team = await this.FindTeamAsync(id);
            if (team != null)
            {
                context.Team.Remove(team);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Team> FindTeamAsync(int id)
        {
            return await context.Team.Include(t1 => t1.Pelada)
                            .Where(t2 => t2.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<TeamView>> FindTeamsAsync()
        {
            return await context.Team
                                .Select(team => new TeamView
                                {
                                    Id = team.Id,
                                    Name = team.Name,
                                    PeladaId = team.PeladaId
                                })
                                .ToListAsync();
        }

        public async Task<Team> CreateAndReturnTeam(Team t)
        {

            await this.CreateTeamAsync(t);
            var team = await this.FindTeamByNameAndPelada(t.Name, t.PeladaId);

            if (team != null)
                return team;

            return null;
        }

        public void CheckReserveBank(string[] arrayOfQuantity)
        {   
            int numberAfterComma = 0;

            if (arrayOfQuantity.Length > 1)
            {
               numberAfterComma = Convert.ToInt32(arrayOfQuantity[1]);
            }

            this.IsReserve = (numberAfterComma != 0);
        }

        public string[] getArrayQuantityTeams(int numberPlayersTeam, int numberAthletes)
        {
            double quantity =
                this.getQuantityOfTeams(numberAthletes, numberPlayersTeam);

            string quantityInString = quantity.ToString();

            return quantityInString.Split('.', System.StringSplitOptions.None);
        }

        private double getQuantityOfTeams(double numberAthletes, double numberPlayersTeam)
        {
            return numberAthletes / numberPlayersTeam;
        }

        private async Task<Team> FindTeamByNameAndPelada(string name, int peladaId)
        {
            return await context.Team
                        .Where(t => (t.Name == name && t.PeladaId == peladaId))
                        .FirstOrDefaultAsync();
        }
    }
}
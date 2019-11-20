using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_service.database;
using web_service.Models;

namespace web_service.Repositories
{
    public class TeamRepository
    {
        private readonly WebServiceContext context;

        public TeamRepository(WebServiceContext context)
        {
            this.context = context;
        }

        public async Task<Team> FindTeamAsync(int id)
        {
            return await context.Team.FindAsync(id);
        }

        public async Task<List<Team>> FindTeamsAsync()
        {
            return await context.Team.ToListAsync();
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
    }
}
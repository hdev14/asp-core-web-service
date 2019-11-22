using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task CreatePeladaAsync(Pelada p)
        {
            var list = p.Teams.ToList();

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

        public async Task<Pelada> FindPeladaAsync(int id)
        {
            return await context.Pelada.Where(p => p.Id == id)
                        .Select(pelada => this.GetPeladaWithoutUserPassword(pelada))
                        .FirstOrDefaultAsync();
        }

        public async Task<List<Pelada>> FindPeladasAsync()
        {
            return await context.Pelada.ToListAsync();
        }

        private Pelada GetPeladaWithoutUserPassword(Pelada pelada)
        {
            return new Pelada
            {
                Id = pelada.Id,
                Title = pelada.Title,
                Description = pelada.Description,

                UserId = pelada.UserId,
                User = this.GetPeladaUser(pelada.UserId),

                SportId = pelada.SportId,
                Sport = pelada.Sport,

                Teams = this.GetPeladaTeams(pelada.Id)

            };
        }

        private User GetPeladaUser(int userId)
        {
            return context.User.Where(u => u.Id == userId)
                                .Select(user => new User
                                {
                                    Id = user.Id,
                                    Name = user.Name,
                                    Username = user.Username,
                                }).FirstOrDefault();
        }

        private List<Team> GetPeladaTeams(int peladaId)
        {
            return context.Team.Where(t => t.PeladaId == peladaId).ToList();
        }
    }
}
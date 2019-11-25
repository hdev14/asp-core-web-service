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
            return await context.Athlete.Where(a => a.Id == id)
                                    .Include(a => a.Team)
                                    .FirstOrDefaultAsync();
        }

        public async Task<List<AthleteView>> FindAthletesAsync()
        {
            return await context.Athlete
                                .Select(athlete => new AthleteView
                                {
                                    Id = athlete.Id,
                                    Name = athlete.Name,
                                    TeamId = athlete.TeamId
                                })
                                .ToListAsync();
        }

        public Stack<Athlete> CreateStackOfAthletes(List<Athlete> athletes)
        {
            Stack<Athlete> stackAthletes = new Stack<Athlete>();
            Random r = new Random();

            for (int h = 0; h < athletes.Count; h++)
            {   
                int randomIndex = r.Next(0, athletes.Count);
                
                var athlete = athletes[randomIndex];
                stackAthletes.Push(athlete);
                athletes.Remove(athlete);
            } 
            
            return stackAthletes;
        }
    }
}
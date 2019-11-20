using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_service.database;
using web_service.Models;

namespace web_service.Repositories
{
    public class TimeRepository
    {
        /*
        private readonly WebServiceContext context;

        public TimeRepository(WebServiceContext context)
        {
            this.context = context;
        }

        public async Task CreateTimeAsync(Time time)
        {
            context.Times.Add(time);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTimeAsync(int id, Time t)
        {
            var time = await this.FindTimeAsync(id);
            if (time != null)
            {
                time.Nome = t.Nome;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteTimeAsync(int id)
        {
            var time = await this.FindTimeAsync(id);
            if (time != null)
            {
                context.Times.Remove(time);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Time> FindTimeAsync(int id)
        {
            return await context.Times.FindAsync(id);
        }

        public async Task<List<Time>> GetTimesAsync()
        {
            return await context.Times.ToListAsync();
        }
        */
    }
}
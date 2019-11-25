using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_service.database;
using web_service.Models;
using web_service.ModelsView;

namespace web_service.Repositories
{
    public class UserRepository
    {
        private readonly WebServiceContext context;

        public UserRepository(WebServiceContext context)
        {
            this.context = context;
        }

        public async Task CreateUsuarioAsync(User u)
        {
            context.User.Add(u);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateUserAsync(int id, User u)
        {

            var user = await this.FindUserAsync(id);

            if (user != null)
            {
                user.Name = u.Name;
                user.Username = u.Username;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await this.FindUserAsync(id);

            if (user != null)
            {
                context.User.Remove(user);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<User> FindUserAsync(int id)
        {
            return await context.User.Where(u => u.Id == id)
                                    .Include(u => u.Peladas)
                                    .FirstOrDefaultAsync();
        }

        public async Task<List<UserView>> FindUsersAsync()
        {
            return await context.User
                                .Select(user => new UserView
                                {
                                    Id = user.Id,
                                    Name = user.Name,
                                    Username = user.Username
                                })
                                .ToListAsync();
        }
        
        public async Task<User> GetUserByUsername(string username)
        {
            return await context.User.Where(u => u.Username == username).FirstOrDefaultAsync();
        }

    }
}
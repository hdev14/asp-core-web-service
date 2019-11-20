using Microsoft.EntityFrameworkCore;
using web_service.Models;

namespace web_service.database
{
    public class WebServiceContext : DbContext
    {
        // ENTIDADES
        public DbSet<User> User { get; set; }
        public DbSet<Pelada> Pelada { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Athlete> Athlete { get; set; }
        public DbSet<Sport> Sport { get; set; }


        public WebServiceContext(DbContextOptions<WebServiceContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        }


    }
}
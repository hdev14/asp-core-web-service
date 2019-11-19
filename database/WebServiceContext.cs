using Microsoft.EntityFrameworkCore;
using web_service.Models;

namespace web_service.database
{
    public class WebServiceContext : DbContext
    {
        // ENTIDADES
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pelada> Peladas { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Atleta> Atletas { get; set; }
        public DbSet<Esporte> Esportes { get; set; }


        public WebServiceContext(DbContextOptions<WebServiceContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Username).IsUnique();
        }


    }
}
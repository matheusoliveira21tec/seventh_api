using Microsoft.EntityFrameworkCore;
using Seventh_api.Models;

namespace Seventh_api.Data
{
    public class AppBdContext : DbContext
    {
        public AppBdContext(DbContextOptions<AppBdContext> opt) : base(opt)
        {

        }

        public DbSet<Servidor> Servidores { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Servidor>()
                .HasMany(c => c.Videos)
                .WithOne(a => a.Servidor!)
                .HasForeignKey(a => a.IdServidor);

            modelBuilder
                .Entity<Video>()
                .HasOne(a => a.Servidor)
                .WithMany(c => c.Videos)
                .HasForeignKey(a => a.IdServidor);
        }
    }
}
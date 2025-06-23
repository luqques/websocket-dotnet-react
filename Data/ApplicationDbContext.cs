using Microsoft.EntityFrameworkCore;
using User.Websocket.Api.Models;

namespace User.Websocket.Api.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios => Set<Usuario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nome).HasColumnName("nome");
                entity.Property(e => e.Email).HasColumnName("email");
            });
        }
    }
}

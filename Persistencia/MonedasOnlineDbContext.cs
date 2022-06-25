using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Dominio;
namespace Persistencia
{
    public class MonedasOnlineDbContext : IdentityDbContext<Usuario>
    {
        public MonedasOnlineDbContext(DbContextOptions options):base (options){

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            //ARCHIVO DE MIGRACION
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tipo_Cambio>()
                       .HasOne(m => m.MonedaDestino)
                       .WithMany(t => t.cambiosDestino)
                       .HasForeignKey(m => m.MonedaDestinoId)
                       .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Tipo_Cambio>()
                        .HasOne(m => m.MonedaOrigen)
                        .WithMany(t => t.cambiosOrigen)
                        .HasForeignKey(m => m.MonedaOrigenId)
                        .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Dominio.Tipo_Cambio>().HasKey(ci => new { ci.MonedaDestinoId, ci.MonedaOrigenId });
            modelBuilder.Entity<Moneda>()
                          .HasIndex(u => u.Abreviacion)
                          .IsUnique();
            modelBuilder.Entity<Tipo_Cambio>()
                         .HasIndex(u => u.Tipo_Cambio_Id)
                         .IsUnique();
        }
        public DbSet<Moneda> Monedas{set;get;}
        public DbSet<Tipo_Cambio> Tipo_Cambios {set;get;}
        public DbSet<Usuario> Usuario {set;get;}
    }
}
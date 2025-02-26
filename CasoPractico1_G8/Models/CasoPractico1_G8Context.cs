using Microsoft.EntityFrameworkCore;

namespace CasoPractico1_G8.Models
{
    public class CasoPractico1_G8Context : DbContext
    {
        public CasoPractico1_G8Context(DbContextOptions<CasoPractico1_G8Context> options) : base(options) { }

        // Tablas o Entidades
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Ruta> Ruta { get; set; }
        public DbSet<Vehiculo> Vehiculo { get; set; }
        public DbSet<Boleto> Boleto { get; set; }
        public DbSet<Parada> Parada { get; set; }
        public DbSet<Horario> Horario { get; set; }

        // Configuración de las relaciones y propiedades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ruta>(Ruta =>
            {
                Ruta.HasKey(r => r.Id);
                Ruta.Property(r => r.Nombre).IsRequired().HasMaxLength(100);
                Ruta.Property(r => r.CodigoRuta).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<Usuario>(Usuario =>
            {
                Usuario.HasKey(u => u.Id);
                Usuario.ToTable("Usuario");
                Usuario.Property(u => u.NombreUsuario).HasMaxLength(50).IsRequired();
                Usuario.Property(u => u.Correo).HasMaxLength(100);
            });

            modelBuilder.Entity<Vehiculo>(Vehiculo =>
            {
                Vehiculo.HasKey(v => v.Id);
            });

            modelBuilder.Entity<Boleto>(Boleto =>
            {
                Boleto.HasKey(b => b.Id);
            });

            modelBuilder.Entity<Parada>(Parada =>
            {
                Parada.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Horario>(Horario =>
            {
                Horario.HasKey(h => h.Id);
            });

            // Configuración de relaciones
            modelBuilder.Entity<Boleto>()
                .HasOne(b => b.Usuario)
                .WithMany(u => u.BoletosComprados)
                .HasForeignKey(b => b.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Boleto>()
                .HasOne(b => b.Ruta)
                .WithMany(r => r.BoletosVendidos)
                .HasForeignKey(b => b.RutaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Parada>()
                .HasOne(p => p.Ruta)
                .WithMany(r => r.Paradas)
                .HasForeignKey(p => p.RutaId);

            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Ruta)
                .WithMany(r => r.Horarios)
                .HasForeignKey(h => h.RutaId);
        }
    }
}

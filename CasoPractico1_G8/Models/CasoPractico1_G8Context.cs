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
        public DbSet<RutaParada> RutaParada { get; set; }
        public DbSet<RutaHorario> RutaHorario { get; set; }

        // Configuración de las relaciones y propiedades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la entidad Usuario
            modelBuilder.Entity<Usuario>(Usuario =>
            {
                Usuario.HasKey(u => u.Id);
                Usuario.ToTable("Usuario");
                Usuario.Property(u => u.NombreUsuario).HasMaxLength(50).IsRequired();
                Usuario.Property(u => u.Correo).HasMaxLength(100);
            });

            // Configuración de la entidad Ruta
            modelBuilder.Entity<Ruta>(Ruta =>
            {
                Ruta.HasKey(r => r.Id);
                Ruta.Property(r => r.Nombre).IsRequired().HasMaxLength(100);
                Ruta.Property(r => r.CodigoRuta).IsRequired().HasMaxLength(20);
            });

            // Configuración de la entidad Vehículo
            modelBuilder.Entity<Vehiculo>(Vehiculo =>
            {
                Vehiculo.HasKey(v => v.Id);
            });

            // Configuración de la entidad Boleto
            modelBuilder.Entity<Boleto>(Boleto =>
            {
                Boleto.HasKey(b => b.Id);

                Boleto.HasOne(b => b.Usuario)
                    .WithMany(u => u.BoletosComprados)
                    .HasForeignKey(b => b.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);

                Boleto.HasOne(b => b.Ruta)
                    .WithMany(r => r.BoletosVendidos)
                    .HasForeignKey(b => b.RutaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de la entidad Parada
            modelBuilder.Entity<Parada>(Parada =>
            {
                Parada.HasKey(p => p.Id);
            });

            // Configuración de la entidad Horario
            modelBuilder.Entity<Horario>(Horario =>
            {
                Horario.HasKey(h => h.Id);
            });

            // Configuración de la tabla intermedia RutaParada (Relación muchos a muchos entre Ruta y Parada)
            modelBuilder.Entity<RutaParada>()
                .HasKey(rp => new { rp.RutaId, rp.ParadaId });

            modelBuilder.Entity<RutaParada>()
                .HasOne(rp => rp.Ruta)
                .WithMany(r => r.Paradas)
                .HasForeignKey(rp => rp.RutaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RutaParada>()
                .HasOne(rp => rp.Parada)
                .WithMany(p => p.Rutas)
                .HasForeignKey(rp => rp.ParadaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de la tabla intermedia RutaHorario (Relación muchos a muchos entre Ruta y Horario)
            modelBuilder.Entity<RutaHorario>()
                .HasKey(rh => new { rh.RutaId, rh.HorarioId });

            modelBuilder.Entity<RutaHorario>()
                .HasOne(rh => rh.Ruta)
                .WithMany(r => r.Horarios)
                .HasForeignKey(rh => rh.RutaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RutaHorario>()
                .HasOne(rh => rh.Horario)
                .WithMany(h => h.Rutas)
                .HasForeignKey(rh => rh.HorarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

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

        // Configuración de las relaciones y propiedades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la entidad Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.ToTable("Usuario");

                entity.Property(u => u.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.NombreCompleto)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Correo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Telefono)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(u => u.Contraseña)
                    .IsRequired();

                entity.Property(u => u.Rol)
                    .IsRequired();
            });

            // Configuración de la entidad Ruta
            modelBuilder.Entity<Ruta>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.ToTable("Ruta");

                entity.Property(r => r.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(r => r.Descripcion)
                    .HasMaxLength(300);

                entity.Property(r => r.Estado)
                    .IsRequired();

                entity.Property(r => r.FechaRegistro)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(r => r.UsuarioRegistro)
                    .WithMany()
                    .HasForeignKey(r => r.UsuarioRegistroId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configuración de la entidad Vehículo
            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.ToTable("Vehiculo");

                entity.Property(v => v.Placa)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(v => v.Modelo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(v => v.CapacidadPasajeros)
                    .IsRequired();

                entity.Property(v => v.Estado)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(v => v.FechaRegistro)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(v => v.UsuarioRegistro)
                    .WithMany()
                    .HasForeignKey(v => v.UsuarioRegistroId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configuración de la entidad Boleto
            modelBuilder.Entity<Boleto>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.ToTable("Boleto");

                entity.Property(b => b.FechaCompra)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(b => b.Activo)
                    .HasDefaultValue(true);

                entity.HasOne(b => b.Usuario)
                    .WithMany(u => u.BoletosComprados)
                    .HasForeignKey(b => b.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Ruta)
                    .WithMany(r => r.BoletosVendidos)
                    .HasForeignKey(b => b.RutaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
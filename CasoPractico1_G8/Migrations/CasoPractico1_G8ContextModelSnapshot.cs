﻿// <auto-generated />
using System;
using CasoPractico1_G8.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CasoPractico1_G8.Migrations
{
    [DbContext(typeof(CasoPractico1_G8Context))]
    partial class CasoPractico1_G8ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CasoPractico1_G8.Models.Boleto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("FechaCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int?>("RutaId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RutaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Boleto", (string)null);
                });

            modelBuilder.Entity("CasoPractico1_G8.Models.Ruta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioRegistroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.HasIndex("UsuarioRegistroId");

                    b.ToTable("Ruta", (string)null);
                });

            modelBuilder.Entity("CasoPractico1_G8.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("CasoPractico1_G8.Models.Vehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CapacidadPasajeros")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("FechaRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioRegistroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.HasIndex("UsuarioRegistroId");

                    b.ToTable("Vehiculo", (string)null);
                });

            modelBuilder.Entity("CasoPractico1_G8.Models.Boleto", b =>
                {
                    b.HasOne("CasoPractico1_G8.Models.Ruta", "Ruta")
                        .WithMany("BoletosVendidos")
                        .HasForeignKey("RutaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CasoPractico1_G8.Models.Usuario", "Usuario")
                        .WithMany("BoletosComprados")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ruta");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("CasoPractico1_G8.Models.Ruta", b =>
                {
                    b.HasOne("CasoPractico1_G8.Models.Usuario", null)
                        .WithMany("RutasRegistradas")
                        .HasForeignKey("UsuarioId");

                    b.HasOne("CasoPractico1_G8.Models.Usuario", "UsuarioRegistro")
                        .WithMany()
                        .HasForeignKey("UsuarioRegistroId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("UsuarioRegistro");
                });

            modelBuilder.Entity("CasoPractico1_G8.Models.Vehiculo", b =>
                {
                    b.HasOne("CasoPractico1_G8.Models.Usuario", null)
                        .WithMany("VehiculosRegistrados")
                        .HasForeignKey("UsuarioId");

                    b.HasOne("CasoPractico1_G8.Models.Usuario", "UsuarioRegistro")
                        .WithMany()
                        .HasForeignKey("UsuarioRegistroId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("UsuarioRegistro");
                });

            modelBuilder.Entity("CasoPractico1_G8.Models.Ruta", b =>
                {
                    b.Navigation("BoletosVendidos");
                });

            modelBuilder.Entity("CasoPractico1_G8.Models.Usuario", b =>
                {
                    b.Navigation("BoletosComprados");

                    b.Navigation("RutasRegistradas");

                    b.Navigation("VehiculosRegistrados");
                });
#pragma warning restore 612, 618
        }
    }
}

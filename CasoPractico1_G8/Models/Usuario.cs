using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CasoPractico1_G8.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Nombre de usuario")]
        public string NombreUsuario { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        [Required, MaxLength(20)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public string Rol { get; set; } // Administrador, Conductor, Usuario

        // Relaciones
        public IEnumerable<Ruta>? RutasRegistradas { get; set; }
        public IEnumerable<Vehiculo>? VehiculosRegistrados { get; set; }
        public IEnumerable<Boleto>? BoletosComprados { get; set; }
    }
}

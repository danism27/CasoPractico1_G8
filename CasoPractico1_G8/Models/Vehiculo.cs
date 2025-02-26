using System;
using System.ComponentModel.DataAnnotations;

namespace CasoPractico1_G8.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        [Display(Name = "Placa del vehículo")]
        public string Placa { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Modelo del vehículo")]
        public string Modelo { get; set; }

        [Required]
        [Display(Name = "Capacidad de pasajeros")]
        public int CapacidadPasajeros { get; set; }

        [Required]
        [Display(Name = "Estado del vehículo")]
        public string Estado { get; set; } // Bueno, Regular, Necesita mantenimiento

        [Display(Name = "Fecha de registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Usuario que registró")]
        public int UsuarioRegistroId { get; set; }
        public Usuario UsuarioRegistro { get; set; }
    }
}

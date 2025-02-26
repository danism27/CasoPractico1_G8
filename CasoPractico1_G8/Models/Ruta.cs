using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CasoPractico1_G8.Models
{
    public class Ruta
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        [Display(Name = "Código de la ruta")]
        public string CodigoRuta { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Nombre de la ruta")]
        public string Nombre { get; set; }

        [MaxLength(300)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Usuario que registró")]
        public int UsuarioRegistroId { get; set; }
        public Usuario UsuarioRegistro { get; set; }

        // Relaciones
        public IEnumerable<Parada>? Paradas { get; set; }
        public IEnumerable<Horario>? Horarios { get; set; }
        public IEnumerable<Boleto>? BoletosVendidos { get; set; }
    }
}

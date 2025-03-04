using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasoPractico1_G8.Models
{
    public class Ruta
    {
        [Key]
        [Required]
        [Display(Name = "Código de la ruta")]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Nombre de la ruta")]
        public string Nombre { get; set; }

        [MaxLength(300)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Se asegura que la clave foránea sea nullable para permitir ON DELETE SET NULL
        [ForeignKey("UsuarioRegistro")]
        [Display(Name = "Usuario que registró")]
        public int? UsuarioRegistroId { get; set; }

        // Relación con la tabla Usuario
        public Usuario? UsuarioRegistro { get; set; }

        [NotMapped]
        [Display(Name = "Paradas")]
        public string ParadasTexto { get; set; } = string.Empty;

        [NotMapped]
        [Display(Name = "Horarios")]
        public string HorariosTexto { get; set; } = string.Empty;

        public List<string> Paradas => string.IsNullOrWhiteSpace(ParadasTexto) ? new List<string>() : new List<string>(ParadasTexto.Split(','));
        public List<string> Horarios => string.IsNullOrWhiteSpace(HorariosTexto) ? new List<string>() : new List<string>(HorariosTexto.Split(','));

        // Relación con Boletos
        public IEnumerable<Boleto>? BoletosVendidos { get; set; }
    }
}

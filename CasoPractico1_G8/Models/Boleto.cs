using System;
using System.ComponentModel.DataAnnotations;

namespace CasoPractico1_G8.Models
{
    public class Boleto
    {
        public int Id { get; set; }

        [Required]
        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [Required]
        public int? RutaId { get; set; }
        public Ruta? Ruta { get; set; }

        public DateTime FechaCompra { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;
    }
}

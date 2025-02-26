using System.ComponentModel.DataAnnotations;

namespace CasoPractico1_G8.Models
{
    public class Parada
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Nombre de la parada")]
        public string Nombre { get; set; }

        [Required]
        public int RutaId { get; set; }
        public Ruta Ruta { get; set; }
    }
}

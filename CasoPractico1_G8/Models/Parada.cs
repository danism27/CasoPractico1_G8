using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CasoPractico1_G8.Models
{
    public class Parada
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Nombre de la parada")]
        public string Nombre { get; set; }

        [MaxLength(300)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public ICollection<RutaParada>? Rutas { get; set; }
    }
}

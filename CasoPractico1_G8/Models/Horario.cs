using System;
using System.ComponentModel.DataAnnotations;

namespace CasoPractico1_G8.Models
{
    public class Horario
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Hora de salida")]
        public TimeSpan HoraSalida { get; set; }

        [Required]
        public int RutaId { get; set; }
        public Ruta Ruta { get; set; }
    }
}

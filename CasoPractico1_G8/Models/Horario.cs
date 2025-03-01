using System;
using System.Collections.Generic;
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
        [Display(Name = "Hora de llegada")]
        public TimeSpan HoraLlegada { get; set; }

        public ICollection<RutaHorario>? Rutas { get; set; }
    }
}

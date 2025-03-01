namespace CasoPractico1_G8.Models
{
    public class RutaHorario
    {
        public int RutaId { get; set; }
        public Ruta Ruta { get; set; }

        public int HorarioId { get; set; }
        public Horario Horario { get; set; }
    }
}

namespace CasoPractico1_G8.Models
{
    public class RutaParada
    {
        public int RutaId { get; set; }
        public Ruta Ruta { get; set; }

        public int ParadaId { get; set; }
        public Parada Parada { get; set; }
    }
}

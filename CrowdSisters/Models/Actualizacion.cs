namespace CrowdSisters.Models
{
    public class Actualizacion
    {
        public int IDActualizacion { get; set; }
        public int FKProyecto { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaActualizacion { get; set; }

        // Navigation property
        public Proyecto Proyecto { get; set; }
    }
}

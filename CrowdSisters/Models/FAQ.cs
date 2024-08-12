namespace CrowdSisters.Models
{
    public class FAQ
    {
        public int IDComentario { get; set; }
        public int FKProyecto { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaComentario { get; set; }
        public string Titulo { get; set; }

        // Navigation property
        public Proyecto Proyecto { get; set; }
    }
}

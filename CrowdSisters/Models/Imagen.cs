namespace CrowdSisters.Models
{
    public class Imagen
    {
        public int IDImagen { get; set; }
        public int FKProyecto { get; set; }
        public string URLImagenProyecto { get; set; }

        // Navigation property
        public Proyecto Proyecto { get; set; }
    }
}

namespace CrowdSisters.Models
{
    public class Usuario
    {
        public int IDUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool IsAdmin { get; set; }
        public string PerfilPublico { get; set; }
        public string URLImagenUsuario { get; set; }
        public decimal? Monedero { get; set; }

        // Navigation properties
        public ICollection<Proyecto> Proyectos { get; set; }
        public ICollection<Donacion> Donaciones { get; set; }

        
    }
}

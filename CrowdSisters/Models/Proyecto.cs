using static System.Net.Mime.MediaTypeNames;

namespace CrowdSisters.Models
{
    public class Proyecto
    { 
    public int IDProyecto { get; set; }
    public int FKUsuario { get; set; }
    public int FKSubcategoria { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaFinalizacion { get; set; }
    public decimal MontoObjetivo { get; set; }
    public decimal MontoRecaudado { get; set; } = 0;
    public int Estado { get; set; }

    // Navigation properties
    public Usuario Usuario { get; set; }
    public Subcategoria Subcategoria { get; set; }
    public ICollection<Actualizacion> Actualizaciones { get; set; }
    public ICollection<Donacion> Donaciones { get; set; }
    public ICollection<FAQ> FAQs { get; set; }
    public ICollection<Imagen> Imagenes { get; set; }
    public ICollection<Recompensa> Recompensas { get; set; }
}
}

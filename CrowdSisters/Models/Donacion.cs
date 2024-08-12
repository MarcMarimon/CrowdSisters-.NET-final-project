namespace CrowdSisters.Models
{
    public class Donacion
    {
        public int IDDonacion { get; set; }
        public int FKProyecto { get; set; }
        public int FKUsuario { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaDonacion { get; set; }
        public int MetodoPago { get; set; }

        // Navigation properties
        public Proyecto Proyecto { get; set; }
        public Usuario Usuario { get; set; }

    }
}

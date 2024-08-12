using System;
namespace CrowdSisters.Models
{
    public class Recompensa
    {
        public int IDRecompensa { get; set; }
        public int FKProyecto { get; set; }
        public string Descripcion { get; set; }
        public decimal MontoMinimo { get; set; }
        public decimal? MontoMaximo { get; set; }
        public int CantidadDisponible { get; set; }
        public string URLImagenRecompensa { get; set; }

        // Navigation property
        public Proyecto Proyecto { get; set; }
    }
}
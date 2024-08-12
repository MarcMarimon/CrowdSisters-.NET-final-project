using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrowdSisters.Models
{
    [Table("Recompensa")]
    public class Recompensa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDRecompensa { get; set; }

        [Required]
        [ForeignKey("Proyecto")]
        public int FKProyecto { get; set; }

        public Proyecto Proyecto { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal MontoMinimo { get; set; }

        [Column(TypeName = "money")]
        public decimal? MontoMaximo { get; set; }

        [Required]
        public int CantidadDisponible { get; set; }

        [Required]
        [StringLength(200)]
        public string URLImagenRecompensa { get; set; }
    }
}

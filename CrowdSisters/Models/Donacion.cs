using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CrowdSisters.Models
{
    [Table("Donacion")]
    public class Donacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDDonacion { get; set; }

        [Required]
        [ForeignKey("Proyecto")]
        public int FKProyecto { get; set; }

        public Proyecto Proyecto { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int FKUsuario { get; set; }

        public Usuario Usuario { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Monto { get; set; }

        [Required]
        public DateTime FechaDonacion { get; set; }

        [Required]
        public int MetodoPago { get; set; }
    }
}

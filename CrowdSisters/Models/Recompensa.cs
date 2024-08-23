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
        public Proyecto Proyecto { get; set; } = new Proyecto();

        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Monto { get; set; }

        [Required]
        [StringLength(200)]
        public string URLImagenRecompensa { get; set; }

        public ICollection<Donacion> Donaciones { get; set; }
    }
}

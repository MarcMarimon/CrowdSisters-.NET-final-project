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
        [ForeignKey("Recompensa")]
        public int FKRecompensa { get; set; }
        public Recompensa Recompensa { get; set; }
    }
}

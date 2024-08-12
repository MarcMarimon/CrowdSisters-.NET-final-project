using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CrowdSisters.Models
{
    [Table("Imagen")]
    public class Imagen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDImagen { get; set; }

        [Required]
        [ForeignKey("Proyecto")]
        public int FKProyecto { get; set; }

        public Proyecto Proyecto { get; set; }

        [Required]
        [StringLength(200)]
        public string URLImagenProyecto { get; set; }
    }
}

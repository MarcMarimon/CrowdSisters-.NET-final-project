using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrowdSisters.Models
{
    [Table("Subcategoria")]
    public class Subcategoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDSubcategoria { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [ForeignKey("Categoria")]
        public int FKCategoria { get; set; }

        public Categoria Categoria { get; set; }

        public ICollection<Proyecto> Proyectos { get; set; }
    }
}

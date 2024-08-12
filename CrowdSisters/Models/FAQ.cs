using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrowdSisters.Models
{
    [Table("FAQ")]
    public class FAQ
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDComentario { get; set; }

        [Required]
        [ForeignKey("Proyecto")]
        public int FKProyecto { get; set; }

        public Proyecto Proyecto { get; set; }

        [Required]
        [StringLength(200)]
        public string Contenido { get; set; }

        [Required]
        public DateTime FechaComentario { get; set; }

        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }
    }
}
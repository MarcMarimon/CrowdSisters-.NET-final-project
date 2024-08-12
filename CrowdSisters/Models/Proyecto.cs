using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrowdSisters.Models
{
    [Table("Proyecto")]
    public class Proyecto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDProyecto { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int FKUsuario { get; set; }

        public Usuario Usuario { get; set; }

        [Required]
        [ForeignKey("Subcategoria")]
        public int FKSubcategoria { get; set; }

        public Subcategoria Subcategoria { get; set; }

        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public DateTime FechaFinalizacion { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal MontoObjetivo { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal MontoRecaudado { get; set; } = 0;

        [Required]
        public int Estado { get; set; }

        public ICollection<Actualizacion> Actualizaciones { get; set; }
        public ICollection<Donacion> Donaciones { get; set; }
        public ICollection<Recompensa> Recompensas { get; set; }
        public ICollection<Imagen> Imagenes { get; set; }
        public ICollection<FAQ> FAQs { get; set; }
    }
}
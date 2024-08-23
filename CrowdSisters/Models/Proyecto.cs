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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDProyecto { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int FKUsuario { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();

        [Required]
        [ForeignKey("Subcategoria")]
        public int FKSubcategoria { get; set; }
        public Subcategoria Subcategoria { get; set; } = new Subcategoria();

        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(500)]
        public string DescripcionGeneral { get; set; }

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

        public int? Estado { get; set; }

        [Required]
        [StringLength(150)]
        public string Subtitulo { get; set; }

        [Required]
        [StringLength(500)]
        public string DescripcionFinalidad { get; set; }

        [Required]
        [StringLength(500)]
        public string DescripcionPresupuesto { get; set; }

        [Required]
        [StringLength(200)]
        public string UrlFotoEncabezado { get; set; }

        [Required]
        [StringLength(200)]
        public string UrlFoto1 { get; set; }

        [Required]
        [StringLength(200)]
        public string UrlFoto2 { get; set; }

        [Required]
        [StringLength(200)]
        public string UrlFoto3 { get; set; }

        public ICollection<Donacion> Donaciones { get; set; } = new List<Donacion>();
        public ICollection<Recompensa> Recompensas { get; set; } = new List<Recompensa>();
    }
}

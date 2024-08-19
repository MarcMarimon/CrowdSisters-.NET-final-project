using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CrowdSisters.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Contrasena { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [StringLength(200)]
        public string PerfilPublico { get; set; }

        [StringLength(200)]
        public string URLImagenUsuario { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Monedero { get; set; } = 0;

        [StringLength(50)]
        public string PrimerApellido { get; set; }

        [StringLength(50)]
        public string SegundoApellido { get; set; }

        [StringLength(10)]
        public string DNI { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        [StringLength(5)]
        public string CodigoPostal { get; set; }

        [StringLength(50)]
        public string Poblacion { get; set; }

        [StringLength(12)]
        public string Telefono { get; set; }

        [StringLength(50)]
        public string Pais { get; set; }

        [Required]
        [StringLength(50)]
        public string Nick { get; set; }
    }
}

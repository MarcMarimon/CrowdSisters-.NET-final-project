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
    }
}

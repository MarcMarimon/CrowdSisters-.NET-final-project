using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrowdSisters.Models
{
    public class CrearProyectoViewModel
    {
        /*Campos necessarios de usuario*/

        [Required]
        public int IDUsuario { get; set; }

        [Required]
        [StringLength(150)]
        public string NombreApellidos { get; set; }

        [Required]
        [StringLength(2000)]

        public string PerfilPublico { get; set; }

        [Required]
        [StringLength(10)]
        public string DNI { get; set; }

        [Required]
        [StringLength(200)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(5)]
        public string CodigoPostal { get; set; }

        [Required]
        [StringLength(50)]
        public string Poblacion { get; set; }

        [Required]
        [StringLength(12)]
        public string Telefono { get; set; }

        [Required]
        public string Pais { get; set; }

        [Required]
        public IFormFile URLImagenUsuario { get; set; }


        /*Campos necessarios de Proyecto*/

        [Required]
        public int FKSubcategoria { get; set; }

        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(2000)]
        public string DescripcionGeneral { get; set; }

        [Required]
        public DateTime FechaFinalizacion { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal MontoObjetivo { get; set; }

        [Required]
        [StringLength(250)]
        public string Subtitulo { get; set; }

        [Required]
        [StringLength(2000)]
        public string DescripcionFinalidad { get; set; }

        [Required]
        [StringLength(2000)]
        public string DescripcionPresupuesto { get; set; }

        [Required]
        public IFormFile UrlFotoEncabezado { get; set; }

        [Required]
        public IFormFile UrlFoto1 { get; set; }

        [Required]
        public IFormFile UrlFoto2 { get; set; }

        [Required]
        public IFormFile UrlFoto3 { get; set; }



        /*Campos necessarios de recompensas */

        [Required]
        [StringLength(50)]
        public string TituloRecompensa { get; set; }

        [Required]
        [StringLength(200)]
        public string DescripcionRecompensa { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Monto { get; set; }

        [Required]
        public IFormFile URLImagenRecompensa { get; set; }


        [Required]
        [StringLength(50)]
        public string TituloRecompensa1 { get; set; }

        [Required]
        [StringLength(200)]
        public string DescripcionRecompensa1 { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Monto1 { get; set; }

        [Required]
        public IFormFile URLImagenRecompensa1 { get; set; }

        [Required]
        [StringLength(50)]
        public string TituloRecompensa2 { get; set; }

        [Required]
        [StringLength(200)]
        public string DescripcionRecompensa2 { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Monto2 { get; set; }

        [Required]
        public IFormFile URLImagenRecompensa2 { get; set; }

        /*Categoria*/

        [Required]
        public int IDCategoria { get; set; }


    }
}

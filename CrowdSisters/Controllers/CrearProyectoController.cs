using CrowdSisters.Models;
using CrowdSisters.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.PortableExecutable;

namespace CrowdSisters.Controllers
{
    public class CrearProyectoController : Controller
    {
        private readonly ServiceCrearProyecto _serviceCrearProyecto;

        private readonly ServiceCategoria _serviceCategoria;

        private readonly ServiceSubcategoria _serviceSubcategoria;


        public CrearProyectoController(ServiceCrearProyecto serviceCrearProyecto, ServiceCategoria serviceCategoria, ServiceSubcategoria serviceSubcategoria)
        {
            _serviceCrearProyecto = serviceCrearProyecto;
            _serviceCategoria = serviceCategoria;
            _serviceSubcategoria = serviceSubcategoria;

        }

        public async Task<IActionResult> Index()
        {
            /*Comprobar si algun usuario tiene iniciada la session, si no la tiene redireccion directa al Login*/

            /*Sacar toda la información del usuario que tiene iniciada la sessión*/

            ViewBag.Usuario = await _serviceCrearProyecto.CrearProjecteView();

            /*Sacar toda la información de categorias*/

            List<Categoria> listCategoria = (List<Categoria>)await _serviceCategoria.GetAllCategoriasAsync();

            ViewBag.ListCategoria = new SelectList(listCategoria, "IDCategoria", "Nombre");

            /*Sacar toda la información de subcategorias*/

            List<Subcategoria> listSubcategoria = (List<Subcategoria>)await _serviceSubcategoria.GetAllSubcategoriasAsync();

            ViewBag.ListSubcategoria = new SelectList(listSubcategoria, "IDSubcategoria", "Nombre");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(CrearProyectoViewModel model)
        {

            foreach (var modelState in ModelState)
            {
                Console.WriteLine($"Key: {modelState.Key}");
                foreach (var error in modelState.Value.Errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            /*Sacar los campos nombre, primer apellido y segundo apellido */

            var nombresArray = model.NombreApellidos.Split(' ');
            string nombre = nombresArray[0];
            string primerApellido = nombresArray.Length > 1 ? nombresArray[1] : string.Empty;
            string segundoApellido = nombresArray.Length > 2 ? nombresArray[2] : string.Empty;

            /*Subir las fotos a Firebase i sacar las Urls*/

            /*Hacer un update dek usuario*/

            /* Creación del proyecto*/

            Proyecto proyecto = new Proyecto();

            proyecto.FKUsuario = model.IDUsuario;
            proyecto.FKSubcategoria = model.FKSubcategoria;
            proyecto.Titulo = model.Titulo;
            proyecto.Subtitulo = model.Subtitulo;
            proyecto.DescripcionGeneral = model.DescripcionGeneral;
            proyecto.DescripcionFinalidad = model.DescripcionFinalidad;
            proyecto.DescripcionPresupuesto= model.DescripcionPresupuesto;
            proyecto.FechaCreacion = DateTime.Now;
            proyecto.FechaFinalizacion = model.FechaFinalizacion;
            proyecto.MontoObjetivo = model.MontoObjetivo;
            proyecto.UrlFotoEncabezado = "sdfgh";
            proyecto.UrlFoto1 = "dfghjk";
            proyecto.UrlFoto2 = "dfghjk";
            proyecto.UrlFoto3 = "dfghjk";

           await _serviceCrearProyecto.CreateProyectoAsync(proyecto);

            return RedirectToAction("Index", "Proyecto");
        }





       


    }
}

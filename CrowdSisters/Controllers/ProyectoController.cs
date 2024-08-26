using CrowdSisters.DAL;
using CrowdSisters.Models;
using CrowdSisters.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace CrowdSisters.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly ServiceProyecto _serviceProyecto;
        private readonly ServiceCategoria _serviceCategoria;
        private readonly ServiceSubcategoria _serviceSubcategoria;


        public ProyectoController(ServiceProyecto serviceProyecto, ServiceCategoria serviceCategoria, ServiceSubcategoria serviceSubcategoria)
        {
            _serviceProyecto = serviceProyecto;
            _serviceCategoria = serviceCategoria;
            _serviceSubcategoria = serviceSubcategoria;
        }

        // Acción para mostrar la lista de proyectos
        public async Task<IActionResult> Index()
        {
            try
            {
                /*Sacar toda la información de categorias*/

                List<Categoria> listCategoria = (List<Categoria>)await _serviceCategoria.GetAllCategoriasAsync();

                ViewBag.ListCategoria = listCategoria;

                var proyectos = await _serviceProyecto.GetAllProyectosAsync();

                if (proyectos == null || !proyectos.Any())
                    ViewBag.ErrorMessage = "No hay proyectos disponibles en este momento.";
                

                return View(proyectos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la lista de proyectos: {ex.Message}");

                ViewBag.ErrorMessage = "Ocurrió un error al cargar los proyectos. Intente de nuevo más tarde.";
                return View("Error"); 
            }
        }


        [HttpGet]
        public async Task<IActionResult> ProyectosSubcategoria(int subcategoria)
        {
            try
            {

                ViewBag.Subcategoria = await _serviceSubcategoria.GetSubcategoriasByIdAsync(subcategoria);
                List<Categoria> listCategoria = (List<Categoria>)await _serviceCategoria.GetAllCategoriasAsync();
                ViewBag.ListCategoria = listCategoria;

                var proyectos = await _serviceProyecto.GetAllProyectosSubcategoriaAsync(subcategoria);

                if (proyectos == null || !proyectos.Any())
                    ViewBag.ErrorMessage = "No hay proyectos disponibles en este momento.";

                return View(proyectos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la lista de proyectos: {ex.Message}");
                ViewBag.ErrorMessage = "Ocurrió un error al cargar los proyectos. Intente de nuevo más tarde.";
                return View("Error");
            }
        }



    }
}

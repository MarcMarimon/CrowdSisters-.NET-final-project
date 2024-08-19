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


        public ProyectoController(ServiceProyecto serviceProyecto)
        {
            _serviceProyecto = serviceProyecto;
        }

        // Acción para mostrar la lista de proyectos
        public async Task<IActionResult> Index()
        {
            try
            {
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

    }
}

using CrowdSisters.Services;
using Microsoft.AspNetCore.Mvc;
using System;

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

                if (proyectos == null)
                {
                    // Si la lista es null, mostramos un mensaje amigable
                    ViewBag.ErrorMessage = "No se pudieron cargar los proyectos. Intente de nuevo más tarde.";
                    return View("Error"); // Vista personalizada de error
                }

                return View(proyectos);
            }
            catch (Exception ex)
            {
                // Loguear el error si es necesario
                Console.WriteLine($"Error al obtener la lista de proyectos: {ex.Message}");

                // Mostrar una vista de error
                ViewBag.ErrorMessage = "Ocurrió un error al cargar los proyectos. Intente de nuevo más tarde.";
                return View("Error"); // Vista personalizada de error
            }
        }
    }
}

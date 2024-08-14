using CrowdSisters.Services;
using Microsoft.AspNetCore.Mvc;

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
            var proyectos = await _serviceProyecto.GetAllProyectosAsync();
            return View(proyectos);
        }
    }
}

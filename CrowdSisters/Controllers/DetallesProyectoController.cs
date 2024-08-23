using CrowdSisters.Models;
using CrowdSisters.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CrowdSisters.Controllers
{
    public class DetallesProyectoController : Controller
    {
        private readonly ServiceProyecto _serviceProyecto;
        private readonly ILogger<DetallesProyectoController> _logger;

        public DetallesProyectoController(ServiceProyecto serviceProyecto, ILogger<DetallesProyectoController> logger)
        {
            _serviceProyecto = serviceProyecto;
            _logger = logger;
        }

        // Acción para mostrar los detalles de un proyecto
        public async Task<IActionResult> Detalles(int id)
        {
            try
            {
                var proyecto = await _serviceProyecto.GetByIdAsync(id);
                if (proyecto == null)
                {
                    return NotFound("El proyecto no fue encontrado.");
                }
                return View(proyecto);  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los detalles del proyecto con ID {Id}", id);
                return StatusCode(500, "Ocurrió un error al intentar obtener los detalles del proyecto. Por favor, inténtalo de nuevo más tarde.");
            }
        }
    }
}

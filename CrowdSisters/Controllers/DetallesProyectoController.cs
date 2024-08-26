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
        private readonly ServiceDonacion _serviceDonacion;
        private readonly ServiceUsuario _serviceUsuario;
        private readonly ServiceRecompensa _serviceRecompensa;

        public DetallesProyectoController(ServiceProyecto serviceProyecto, ILogger<DetallesProyectoController> logger, ServiceDonacion serviceDonacion,
           ServiceUsuario serviceUsuario, ServiceRecompensa serviceRecompensa )
        {
            _serviceProyecto = serviceProyecto;
            _logger = logger;
            _serviceDonacion = serviceDonacion;
            _serviceUsuario = serviceUsuario;
            _serviceRecompensa = serviceRecompensa;
        }

        // Acción para mostrar los detalles de un proyecto
        public async Task<IActionResult> Detalles(int id)
        {
            try
            {
                var proyecto = await _serviceProyecto.GetByIdAsync(id);

                ViewBag.ListaRecompensas = await _serviceRecompensa.GetRecompensasByIdProyectoAsync(id);

                ViewBag.UsuarioActivo = HttpContext.Session.GetInt32("IdUsuario");


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

        [HttpPost]
        public async Task<IActionResult> Donacion(Donacion model)
        {
            try
            {

                int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

                if (idUsuario == null || idUsuario == 0)
                    return RedirectToAction("Index", "Login");
            
                /*Crear donación*/

                await _serviceDonacion.CrearDonacionAsync(model, (int)HttpContext.Session.GetInt32("IdUsuario"));

                /*Buscar recompensa*/

                Recompensa recompensa = await _serviceRecompensa.GetRecompensaByIdAsync(model.FKRecompensa);

                /*Restar dinero al monedero del usuario*/

                await _serviceUsuario.RestarMonederoUsuarioAsync(recompensa.Monto, (int)HttpContext.Session.GetInt32("IdUsuario"));

                /*Añadir dinero al proyecto*/

                await _serviceProyecto.UpdateMontoRecaudadoAsync(recompensa.Monto, model.FKProyecto);

                return RedirectToAction("Index", "Proyecto");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al intentar obtener los detalles del proyecto. Por favor, inténtalo de nuevo más tarde.");

            }
        }

    }
}

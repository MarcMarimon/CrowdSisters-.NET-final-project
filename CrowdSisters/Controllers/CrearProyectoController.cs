using CrowdSisters.Models;
using CrowdSisters.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrowdSisters.Controllers
{
    public class CrearProyectoController : Controller
    {
        private readonly ServiceCrearProyecto _serviceCrearProyecto;


        public CrearProyectoController(ServiceCrearProyecto serviceCrearProyecto)
        {
            _serviceCrearProyecto = serviceCrearProyecto;
        }

        public async Task<IActionResult> Index()
        {
            /*Comprobar si algun usuario tiene iniciada la session, si no la tiene redireccion directa al Login*/

            /*Sacar toda la información del usuario que tiene iniciada la sessión*/

            ViewBag.Usuario = await _serviceCrearProyecto.CrearProjecteView();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(CrearProyectoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            /*Subir las fotos a Firebase i sacar las Urls*/

            /*Hacer un update dek usuario*/

            /* Creación del proyecto*/

            await _serviceCrearProyecto.CreateProyectoAsync(model.Proyecto);

            return RedirectToAction("Index", "Proyecto");
        }





       


    }
}

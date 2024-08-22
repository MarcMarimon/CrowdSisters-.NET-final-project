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


        private readonly ServiceRecompensa _serviceRecompensa;
        private readonly FirebaseService _serviceFirebase; 


        public CrearProyectoController(ServiceCrearProyecto serviceCrearProyecto, ServiceCategoria serviceCategoria, ServiceRecompensa serviceRecompensa, ServiceSubcategoria serviceSubcategoria, FirebaseService serviceFirebase)

        {
            _serviceCrearProyecto = serviceCrearProyecto;
            _serviceCategoria = serviceCategoria;
            _serviceSubcategoria = serviceSubcategoria;
            _serviceRecompensa = serviceRecompensa;
            _serviceFirebase = serviceFirebase;
        }

        public async Task<IActionResult> Index()
        {
            /*Comprobar si algun usuario tiene iniciada la session, si no la tiene redireccion directa al Login*/

            /*Lista paises*/

            ViewBag.Paises = new PaisesViewModel().Paises;

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

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            /*Sacar los campos nombre, primer apellido y segundo apellido */

            var nombresArray = model.NombreApellidos.Split(' ');
            string nombre = nombresArray[0];
            string primerApellido = nombresArray.Length > 1 ? nombresArray[1] : string.Empty;
            string segundoApellido = nombresArray.Length > 2 ? nombresArray[2] : string.Empty;
            Stream imagen = null;
            /*Subir las fotos a Firebase i sacar las Urls*/



            /*Hacer un update del usuario*/

            Usuario usuario = new Usuario();
            usuario.IDUsuario = model.IDUsuario;
            usuario.Nombre = nombre;
            usuario.PrimerApellido = primerApellido;    
            usuario.SegundoApellido = segundoApellido;
            usuario.PerfilPublico = model.PerfilPublico;
            usuario.DNI = model.DNI;
            usuario.Direccion = model.Direccion;    
            usuario.CodigoPostal = model.CodigoPostal;
            usuario.Poblacion = model.Poblacion;
            usuario.Pais = model.Pais;
            usuario.Telefono = model.Telefono;
            usuario.URLImagenUsuario = "ddsdsdsa";

            await _serviceCrearProyecto.UpdateUsuarioCrearProyecto(usuario);



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
            imagen = model.UrlFotoEncabezado.OpenReadStream();
            proyecto.UrlFotoEncabezado = await _serviceFirebase.subirStorage(imagen, model.UrlFotoEncabezado.FileName);
            imagen = model.UrlFoto1.OpenReadStream();
            proyecto.UrlFoto1 = await _serviceFirebase.subirStorage(imagen, model.UrlFoto1.FileName);
            imagen = model.UrlFoto2.OpenReadStream();
            proyecto.UrlFoto2 = await _serviceFirebase.subirStorage(imagen, model.UrlFoto2.FileName);
            imagen = model.UrlFoto3.OpenReadStream();
            proyecto.UrlFoto3 = await _serviceFirebase.subirStorage(imagen, model.UrlFoto3.FileName);




            proyecto = await _serviceCrearProyecto.CreateProyectoAsync(proyecto);

            Recompensa recompensa = new Recompensa();
            recompensa.Titulo = model.TituloRecompensa;
            recompensa.Descripcion = model.DescripcionRecompensa;
            recompensa.Monto = model.Monto;
            imagen = model.URLImagenRecompensa.OpenReadStream();
            recompensa.URLImagenRecompensa = await _serviceFirebase.subirStorage(imagen, model.URLImagenRecompensa.FileName);
            recompensa.FKProyecto = proyecto.IDProyecto;
            await _serviceRecompensa.CreateRecompensaAsync(recompensa);

            Recompensa recompensa1 = new Recompensa();
            recompensa1.Titulo = model.TituloRecompensa1;
            recompensa1.Descripcion = model.DescripcionRecompensa1;
            recompensa1.Monto = model.Monto1;
            imagen = model.URLImagenRecompensa1.OpenReadStream();
            recompensa1.URLImagenRecompensa = await _serviceFirebase.subirStorage(imagen, model.URLImagenRecompensa1.FileName);
            recompensa1.FKProyecto = proyecto.IDProyecto;
            await _serviceRecompensa.CreateRecompensaAsync(recompensa1);

            Recompensa recompensa2 = new Recompensa();
            recompensa2.Titulo = model.TituloRecompensa2;
            recompensa2.Descripcion = model.DescripcionRecompensa2;
            recompensa2.Monto = model.Monto2;
            imagen = model.URLImagenRecompensa2.OpenReadStream();
            recompensa2.URLImagenRecompensa = await _serviceFirebase.subirStorage(imagen, model.URLImagenRecompensa2.FileName);
            recompensa2.FKProyecto = proyecto.IDProyecto;
            await _serviceRecompensa.CreateRecompensaAsync(recompensa2);



            return RedirectToAction("Index", "Proyecto");
        }

    }
}

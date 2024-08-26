using CrowdSisters.DAL;
using CrowdSisters.Models;
using CrowdSisters.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics.Eventing.Reader;

namespace CrowdSisters.Controllers
{
    public class LoginController : Controller
    {
        private readonly ServiceLogin _serviceLogin;
        private readonly DALUsuario _dalUsuario;

        public LoginController(ServiceLogin serviceLogin, DALUsuario dalUsuario)
        {
            _dalUsuario = dalUsuario;
            _serviceLogin = serviceLogin;
        }

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registro()
        {
            return View();
        }

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(Usuario model)
        {
            
            bool isValidUser = await _serviceLogin.VerifyPasswordAsync(model.Contrasena, await _serviceLogin.VerifyMailAsync(model.Email));

            if (isValidUser)
            {
                Usuario user = await _dalUsuario.GetByIdAsync(await _serviceLogin.VerifyMailAsync(model.Email));
                HttpContext.Session.SetInt32("IdUsuario",user.IDUsuario);
                HttpContext.Session.SetString("Username", user.Nick);
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetInt32("Monedero",(int)user.Monedero);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
        }
        public async Task<ActionResult> Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index","Home"); 
            }
        }

    // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Usuario model)
        {
            await _serviceLogin.CreateAsync(model);
            return RedirectToAction("Index");
        }

        // GET: LoginController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            Usuario usuario = await _serviceLogin.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Usuario user, string ContrasenaHidden)
        {
            if (!ModelState.IsValid)
            {
                // Si hay errores de validación, se vuelve a mostrar el formulario
                return View(user);
            }

            try
            {
                if (string.IsNullOrEmpty(user.Contrasena))
                    user.Contrasena = ContrasenaHidden;

                await _serviceLogin.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            catch
            {
                // Manejo de errores
                return View(user);
            }
        }

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
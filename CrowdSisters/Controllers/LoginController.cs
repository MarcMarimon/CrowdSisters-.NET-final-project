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
                HttpContext.Session.SetString("Monedero",user.Monedero.ToString());
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
            int? ID = HttpContext.Session.GetInt32("IdUsuario");
            if(id == ID)
            {
                Usuario usuario = await _serviceLogin.GetByIdAsync(id);
                return View(usuario);
            }else
            {
                return RedirectToAction("Index","Home");
            }

        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario user, string ContrasenaHidden)
        {
            try
            {
                if (user.Contrasena == null)
                    user.Contrasena = ContrasenaHidden;

                _serviceLogin.UpdateAsync(user);
                return View(user);
            }
            catch
            {
                return View();
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
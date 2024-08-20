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
        public LoginController(ServiceLogin serviceLogin, DALUsuario dalUsuario)
        {
            _serviceLogin = serviceLogin;
        }

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                if (await _serviceLogin.VerifyMail(collection["Email"]))
                {
                    if (await _serviceLogin.VerifyPassword(collection["Contrasena"]))
                    {
                        ViewBag.Login = "LoginCorrecto";
                        return RedirectToAction(nameof(Index),ViewBag);
                    }
                    else
                    {
                        ViewBag.Login = "Contraseña incorrecta";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Login = "Email incorrecto";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
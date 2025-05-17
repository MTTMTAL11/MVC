using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RoboGebV6.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string username, string password)
        {
            // Basit kullanıcı kontrolü (Sadece örnek için)
            if (username == "admin" && password == "mttrobot")
            {
                // Kullanıcıyı kimlik doğrulaması yaparak oturuma dahil et
                FormsAuthentication.SetAuthCookie(username, false);

                return RedirectToAction("Index", "Racers");
            }

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }

        // Çıkış Yap
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index");
        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyAuth;
using PersonalWebsite.Models;

namespace PersonalWebsite.Controllers
{
    [EzAuthorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Login

        [EzAllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Home/Login

        [HttpPost]
        [EzAllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid && Authentication.Login(model.Username, model.Password))
            {
                return RedirectToAction("Index", "Account");
            }

            ViewBag.Message = "Invalid user credentials";
            return View(model);
        }


        //
        // GET: /Home/Logout

        public ActionResult Logout()
        {
            Authentication.Logout();
            return RedirectToAction("Index", "Home");
        }

    }
}

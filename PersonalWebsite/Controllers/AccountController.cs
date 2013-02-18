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
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid && Authentication.Login(model.Username, model.Password))
            {
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "Invalid user credentials.");
            return View(model);
        } 

        //
        // GET: /Home/Logout

        public ActionResult Logout()
        {
            Authentication.Logout();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Make

        [EzAllowAnonymous]
        public ActionResult Make()
        {
            Authentication.UserStore.AddUser("test", "test");
            return RedirectToAction("Login", "Account");            
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}

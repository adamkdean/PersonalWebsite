﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyAuth;
using PersonalWebsite.Models;
using PersonalWebsite.Models.Account;

namespace PersonalWebsite.Controllers
{
    [EzAuthorize]
    public partial class AccountController : Controller
    {
        //
        // GET: /Account/

        public virtual ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Login

        [EzAllowAnonymous]
        public virtual ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [EzAllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginViewModel model, string returnUrl)
        {
            // textboxes filled in & user is valid
            if (ModelState.IsValid && Authentication.Login(model.Username, model.Password))
            {
                return RedirectToLocal(returnUrl);
            }

            // textboxes filled in but user is not valid
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid user credentials.");
                using (var context = new WebsiteContext())
                {
                    context.FailedAttempts.Add(
                        new FailedAttempt {
                            DateAttempted = DateTime.Now,
                            UsernameGiven = model.Username,
                            IPAddress = HttpContext.Request.UserHostAddress
                        }
                    );
                    context.SaveChanges();
                }
            }

            return View(model);
        }

        //
        // GET: /Account/Logout

        public virtual ActionResult Logout()
        {
            Authentication.Logout();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Setup

        [EzAllowAnonymous]
        public virtual ActionResult Setup()
        {
            if (Authentication.UserStore.GetAllUsers().Count == 0)
            {
                string username = "admin", password = "changeme";
                Authentication.UserStore.AddUser(username, password);

                ViewBag.Username = username;
                ViewBag.Password = password;
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/UpdatePassword

        public virtual ActionResult UpdatePassword()
        {
            return View();
        }

        //
        // POST: /Account/UpdatePassword

        [HttpPost]
        [EzAllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult UpdatePassword(UpdatePasswordViewModel model)
        {
            var user = Authentication.CurrentUser;

            if (ModelState.IsValid && Authentication.Login(user.Username, model.CurrentPassword))
            {
                user.Hash = Authentication.HashPassword(model.NewPassword, user.Salt);                 
                Authentication.UserStore.UpdateUserById(user.UserId, user);

                Authentication.Logout();
                if (Authentication.Login(user.Username, model.NewPassword))
                {
                    // everything was changed OK
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Password was not changed for some reason.");
                }
            }

            return View(model);
        }

        #region ChildActions
        [ChildActionOnly]
        [EzAllowAnonymous]
        public virtual PartialViewResult FailedAttempts()
        {
            const int limit = 10;
            var model = new FailedAttemptsViewModel();

            using (var context = new WebsiteContext())
            {
                model.FailedAttempts = (from t in context.FailedAttempts
                                        orderby t.FailedAttemptId descending
                                        select t).Take(limit).ToList();
            }

            return PartialView(model);
        }
        #endregion

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            else return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}

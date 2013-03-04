using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyAuth;
using PersonalWebsite.Models;
using PersonalWebsite.Helpers;

namespace PersonalWebsite.Controllers
{
    public partial class HomeController : Controller
    {
        //
        // GET: /Home/

        public virtual ActionResult Index()
        {
            return View();
        }
    }
}

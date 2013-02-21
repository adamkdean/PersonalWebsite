using EasyAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebsite.Controllers
{
    public class CodeController : Controller
    {
        //
        // GET: /Code/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Code/Manage

        [EzAuthorize]
        public ActionResult Manage()
        {
            return View();
        }

    }
}

using EasyAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebsite.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Blog/Manage

        [EzAuthorize]
        public ActionResult Manage()
        {
            return View();
        }

        //
        // GET: /Blog/New

        [EzAuthorize]
        public ActionResult New()
        {
            return View();
        }

        //
        // GET: /Blog/Delete

        [EzAuthorize]
        public ActionResult Delete()
        {
            return View();
        }

    }
}

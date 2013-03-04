using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebsite.Controllers
{
    public partial class ContactController : Controller
    {
        //
        // GET: /Contact/

        public virtual ActionResult Index()
        {
            return View();
        }

        #region ChildActions
        [ChildActionOnly]
        public virtual PartialViewResult TwitterFeed()
        {
            return PartialView();
        }
        #endregion

    }
}

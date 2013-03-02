using EasyAuth;
using System.Web.Mvc;
using StackExchange.StacMan;
using System;
using System.Collections.Generic;

namespace PersonalWebsite.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/

        [OutputCache(Duration = 60, VaryByParam = "none")]
        public ActionResult Index()
        {
            var site = "stackoverflow";
            var id = new List<int>() { 1138620 };

            var client = new StacManClient();
            
            var response = client.Users.GetByIds(site, id, filter: "!9hnGsshl(");
            var profile = response.Result.Data.Items[0];

            ViewBag.AboutMe = profile.AboutMe;            

            return View();
        }

        //
        // GET: /Profile/Manage

        [EzAuthorize]
        public ActionResult Manage()
        {
            return View();
        }

    }
}

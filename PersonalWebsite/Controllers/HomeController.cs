using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyAuth;
using PersonalWebsite.Models;
using PersonalWebsite.Helpers;
using PersonalWebsite.Models.Home;

namespace PersonalWebsite.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        #region ChildActions
        [ChildActionOnly]
        public PartialViewResult RecentPostsList()
        {            
            var model = new RecentPostsListViewModel();
            model.BlogPosts = BlogPostHelper.GetRecentPosts(5);
            return PartialView(model);
        }
        #endregion
    }
}

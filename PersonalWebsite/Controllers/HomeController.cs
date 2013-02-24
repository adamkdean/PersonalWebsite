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
        public PartialViewResult RecentBlogPostsList()
        {
            var model = BlogPostHelper.GetRecentBlogPosts(5);
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult RecentBlogPostsFull()
        {
            var model = BlogPostHelper.GetRecentBlogPosts(1);
            return PartialView(model);
        }
        
        [ChildActionOnly]
        public PartialViewResult TagCloud()
        {
            var model = new TagsViewModel();
            model.Tags = TagHelper.GetRandomTags(10);
            return PartialView(model);
        }
        #endregion

        
    }
}

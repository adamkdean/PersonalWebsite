using EasyAuth;
using PersonalWebsite.Models;
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
            var model = new BlogPostsViewModel();
            
            using (var context = new WebsiteContext())
            {
                // eagerly load the tags as the context will be disposed
                var posts = (from t in context.BlogPosts.Include("Tags")
                            orderby t.DatePosted descending 
                            select t).ToList();
                model.BlogPosts = posts;
            }

            return View(model);
        }

        //
        // GET: /Blog/New

        [EzAuthorize]
        public ActionResult New()
        {
            return View();
        }

        //
        // POST: /Blog/New

        [HttpPost]
        [EzAuthorize]
        public ActionResult New(NewBlogPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Manage", "Blog");
            }

            return View(model);
        }

        //
        // GET: /Blog/Edit

        [EzAuthorize]
        public ActionResult Edit()
        {
            return View();
        }

        //
        // GET: /Blog/Delete

        [EzAuthorize]
        public ActionResult Delete()
        {
            // are you sure you want to delete? will pop up with JS
            // if yes then it is posted here

            return RedirectToAction("Manage", "Blog");            
        }

        //
        // GET: /Blog/Make

        [EzAuthorize]
        public ActionResult Make()
        {
            using (var context = new WebsiteContext())
            {
                TagHelper.AddTagRange(new string[] { "C#", "HTML", "ASP.NET" });
                TagHelper.AddTag("c#");
                TagHelper.AddTag("html");

                Tag tag1 = context.Tags.Where(x => x.TagName.Equals("C#", StringComparison.OrdinalIgnoreCase)).First();
                Tag tag2 = context.Tags.Where(x => x.TagName.Equals("html", StringComparison.OrdinalIgnoreCase)).First();

                BlogPost post = context.BlogPosts.Create();
                post.BlogTitle = "Test post";
                post.BlogContent = "this is a test blog and as such lorem bacon.";
                post.DatePosted = DateTime.Now;
                post.Tags.Add(tag1);
                post.Tags.Add(tag2);
                context.BlogPosts.Add(post);
                context.SaveChanges();
            }

            return RedirectToAction("Manage");
        }

    }
}

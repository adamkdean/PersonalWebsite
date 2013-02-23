using EasyAuth;
using PersonalWebsite.Helpers;
using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntityFramework.Extensions;

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
                // eagerly load the tags/comments etc as the context will be disposed
                var posts = (from t in context.BlogPosts
                                              .Include("Tags")
                                              .Include("Comments")
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
        [ValidateInput(false)]
        public ActionResult New(NewBlogPostViewModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var tagcsv = "";
                if (!string.IsNullOrEmpty(formCollection["tags"]))
                    tagcsv = formCollection["tags"];
                var tags = TagHelper.GetTagArray(tagcsv);
                
                using (var context = new WebsiteContext())
                {
                    // make sure tags exist in database, if not, they're added
                    TagHelper.AddTagRange(tags);

                    var taglist = new List<Tag>();
                    foreach (string tag in tags)
                    {
                        var tagObject = TagHelper.GetTag(context, tag);
                        taglist.Add(tagObject);
                    }

                    var post = context.BlogPosts.Create();
                    post.BlogTitle = WebHelper.StripTags(model.BlogTitle);
                    post.BlogContent = WebHelper.StripTags(model.BlogContent);
                    post.DatePosted = DateTime.Now;
                    post.Tags.AddRange(taglist);                    
                    context.BlogPosts.Add(post);
                    context.SaveChanges();
                }

                return RedirectToAction("Manage", "Blog");
            }

            return View(model);
        }

        //
        // GET: /Blog/Edit

        [EzAuthorize]
        public ActionResult Edit(int id = -1)
        {
            EditBlogPostViewModel model = new EditBlogPostViewModel();

            using (var context = new WebsiteContext())
            {
                if (!context.BlogPosts.Any(x => x.BlogPostId == id))
                    RedirectToAction("Manage", "Blog");

                // eagerly load the tags/comments etc as the context will be disposed
                var posts = (from t in context.BlogPosts                            
                                             .Include("Tags")
                                             .Include("Comments")
                            where t.BlogPostId == id
                            select t);

                if (posts.Count() > 0) 
                {
                    var post = posts.First();

                    model.BlogPostId = post.BlogPostId;
                    model.BlogTitle = post.BlogTitle;
                    model.BlogContent = post.BlogContent;
                    ViewBag.Tags = TagHelper.GetTagArray(post.Tags);
                }
                else return RedirectToAction("Manage", "Blog");
            }

            return View(model);
        }

        //
        // POST: /Blog/Edit

        [HttpPost]
        [EzAuthorize]
        [ValidateInput(false)]
        public ActionResult Edit(EditBlogPostViewModel model, FormCollection formCollection)
        {
            var tagcsv = "";
            if (!string.IsNullOrEmpty(formCollection["tags"]))
                tagcsv = formCollection["tags"];
            var tags = TagHelper.GetTagArray(tagcsv);

            if (ModelState.IsValid)
            {
                using (var context = new WebsiteContext())
                {
                    if (!context.BlogPosts.Any(x => x.BlogPostId == model.BlogPostId))
                        return RedirectToAction("Manage", "Blog");

                    // make sure tags exist in database, if not, they're added
                    TagHelper.AddTagRange(tags);

                    var taglist = new List<Tag>();
                    foreach (string tag in tags)
                    {
                        var tagObject = TagHelper.GetTag(context, tag);
                        taglist.Add(tagObject);
                    }

                    var post = context.BlogPosts.First(x => x.BlogPostId == model.BlogPostId);
                    post.BlogTitle = WebHelper.StripTags(model.BlogTitle);
                    post.BlogContent = WebHelper.StripTags(model.BlogContent);
                    post.DateModified = DateTime.Now;
                    post.Tags.Clear();
                    post.Tags.AddRange(taglist);                    
                    context.SaveChanges();
                }

                return RedirectToAction("Manage", "Blog");
            }

            ViewBag.Tags = tags;
            return View(model);
        }

        //
        // GET: /Blog/Delete

        [HttpPost]
        [EzAuthorize]
        public ActionResult Delete(FormCollection formCollection)
        {
            List<int> list = new List<int>();
            foreach (string box in formCollection)
            {
                int id = 0;
                if (int.TryParse(formCollection[box], out id))
                    list.Add(id);
            }

            using (var context = new WebsiteContext())
            {
                context.BlogPosts.Delete(x => list.Contains(x.BlogPostId)); 
            }

            return RedirectToAction("Manage", "Blog");            
        }
    }
}

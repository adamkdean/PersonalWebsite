using EasyAuth;
using PersonalWebsite.Helpers;
using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [ValidateInput(false)]
        public ActionResult New(NewBlogPostViewModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                string tagcsv = "";
                if (!string.IsNullOrEmpty(formCollection["item[tags][]"]))
                    tagcsv = formCollection["item[tags][]"];
                string[] tags = TagHelper.GetTagsFromCSV(tagcsv);
                
                using (var context = new WebsiteContext())
                {
                    // make sure tags exist in database, if not, they're added
                    TagHelper.AddTagRange(tags);

                    List<Tag> taglist = new List<Tag>();
                    foreach (string tag in tags)
                    {
                        Tag tagObject = TagHelper.GetTag(context, tag);
                        taglist.Add(tagObject);
                    }

                    BlogPost post = context.BlogPosts.Create();
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
        public ActionResult Edit(int id)
        {
            EditBlogPostViewModel model = new EditBlogPostViewModel();

            using (var context = new WebsiteContext())
            {
                if (!context.BlogPosts.Any(x => x.BlogPostId == id))
                    RedirectToAction("Manage", "Blog");

                // eagerly load the tags as the context will be disposed
                var post = (BlogPost)(from t in context.BlogPosts.Include("Tags")
                                     where t.BlogPostId = id
                                     select t);

                model.BlogPostId = post.BlogPostId;
                model.BlogTitle = post.BlogTitle;
                model.BlogContent = post.BlogContent;
                model.Tags = post.Tags;
            }

            return View(model);
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
    }
}

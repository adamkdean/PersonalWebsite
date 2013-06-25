using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyAuth;
using EntityFramework.Extensions;
using PersonalWebsite.Helpers;
using PersonalWebsite.Models;
using PersonalWebsite.Models.Blog;
using PersonalWebsite.Extensions;

namespace PersonalWebsite.Controllers
{
    public partial class BlogController : Controller
    {
        //
        // GET: /Blog/

        public virtual ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Blog/Read/$id

        public virtual ActionResult Read(int id = -1, string slug = "")
        {
            var model = new ReadViewModel();

            using (var context = new WebsiteContext())
            {
                if (!context.BlogPosts.Any(x => x.BlogPostId == id))
                    RedirectToAction("Index", "Blog");

                // eagerly load the tags etc as the context will be disposed
                var posts = (from t in context.BlogPosts.Include("Tags")                                             
                             where t.BlogPostId == id
                             select t);

                // get the blog post, but increment the views while we're at it!
                model.BlogPost = posts.First();
                model.BlogPost.Views++;
                context.SaveChanges();
            }

            return View(model);
        }

        //
        // GET: /Blog/Tagged/$id

        public virtual ActionResult Tagged(int id = -1, string slug = "")
        {
            var model = new TaggedViewModel();

            using (var context = new WebsiteContext())
            {
                if (!context.Tags.Any(x => x.TagId == id))
                    RedirectToAction("Index", "Blog");

                // eagerly load the blogposts/tags? etc as the context will be disposed
                var query = (from t in context.Tags.Include("BlogPosts.Tags")                                              
                             where t.TagId == id                             
                             select t);
                var tag = (Tag)query.First();
                model.TagName = tag.TagName;
                model.BlogPosts = tag.BlogPosts.OrderByDescending(x => x.DatePosted).ToList();
            }

            return View(model);
        }

        //
        // GET: /Blog/Search

        [HttpPost]        
        public virtual ActionResult Search(SearchViewModel model)
        {
            using (var context = new WebsiteContext())
            {
                // eagerly load the tags etc as the context will be disposed
                var posts = (from t in context.BlogPosts.Include("Tags")
                             where t.BlogContent.Contains(model.SearchTerm) ||
                                   t.BlogTitle.Contains(model.SearchTerm) ||
                                   t.Tags.Any(x => x.TagName == model.SearchTerm)
                             select t);

                model.BlogPosts = posts.OrderByDescending(x => x.DatePosted).ToList();
            }

            return View(model);
        }

        //
        // GET: /Blog/Manage

        [EzAuthorize]
        public virtual ActionResult Manage()
        {
            var model = new ManageViewModel();
            
            using (var context = new WebsiteContext())
            {
                // eagerly load the tags etc as the context will be disposed
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
        public virtual ActionResult New()
        {
            return View();
        }

        //
        // POST: /Blog/New
        
        [HttpPost]
        [EzAuthorize]
        [ValidateInput(false)]
        public virtual ActionResult New(NewViewModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var tagcsv = "";
                if (!string.IsNullOrEmpty(formCollection["tags"]))
                    tagcsv = formCollection["tags"];
                tagcsv = tagcsv.ToLowerInvariant(); // keep your case down bro
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
                    post.BlogContent = model.BlogContent; // WebHelper.StripTags(); we should trust ourselves.
                    post.Slug = post.BlogTitle.Slugify();
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
        public virtual ActionResult Edit(int id = -1)
        {
            var model = new EditViewModel();

            using (var context = new WebsiteContext())
            {
                if (!context.BlogPosts.Any(x => x.BlogPostId == id))
                    RedirectToAction("Manage", "Blog");

                // eagerly load the tags/comments etc as the context will be disposed
                var posts = (from t in context.BlogPosts.Include("Tags")                                             
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
        public virtual ActionResult Edit(EditViewModel model, FormCollection formCollection)
        {
            var tagcsv = "";
            if (!string.IsNullOrEmpty(formCollection["tags"]))
                tagcsv = formCollection["tags"];
            tagcsv = tagcsv.ToLowerInvariant(); // keep your case down bro
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
                    post.BlogContent = model.BlogContent; // WebHelper.StripTags() removed. Trust your admins.
                    post.Slug = post.BlogTitle.Slugify();
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
        public virtual ActionResult Delete(FormCollection formCollection)
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

        #region ChildActions
        [ChildActionOnly]
        public virtual PartialViewResult SinglePost(SinglePostViewModel model)
        {
            return PartialView(model);
        }
        
        [ChildActionOnly]        
        public virtual PartialViewResult RecentPosts()
        {
            var model = new RecentPostsViewModel();
            model.BlogPosts = BlogPostHelper.GetRecentPosts(5);
            return PartialView(model);
        }

        [ChildActionOnly]
        public virtual PartialViewResult RecentPostsList()
        {
            var model = new RecentPostsListViewModel();
            model.BlogPosts = BlogPostHelper.GetRecentPosts(5);
            return PartialView(model);
        }
        
        [ChildActionOnly]
        public virtual PartialViewResult PopularPostsList()
        {
            var model = new PopularPostsListViewModel();
            model.BlogPosts = BlogPostHelper.GetPopularPosts(5);
            return PartialView(model);
        }

        [ChildActionOnly]
        public virtual PartialViewResult Sidebar()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public virtual PartialViewResult PostArchive()
        {
            var model = new PostArchiveViewModel();
            model.BlogPosts = BlogPostHelper.GetAllPosts(loadAssets: false);
            //model.BlogPosts = model.BlogPosts.OrderByDescending(x => x.DatePosted);
            return PartialView(model);
        }

        [ChildActionOnly]
        public virtual PartialViewResult TagCloud()
        {
            var model = new TagCloudViewModel();
            model.Tags = TagHelper.GetTagsByMostPopular();
            return PartialView(model);
        }

        [ChildActionOnly]
        public virtual PartialViewResult SearchBar()
        {
            var model = new SearchViewModel();
            return PartialView(model);
        }
        #endregion
    }
}

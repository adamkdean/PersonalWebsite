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
            var model = new CodePostsViewModel();

            using (var context = new WebsiteContext())
            {
                // eagerly load the tags as the context will be disposed
                var posts = (from t in context.CodePosts.Include("Tags")
                             orderby t.DatePosted descending
                             select t).ToList();
                model.CodePosts = posts;
            }

            return View(model);
        }

        //
        // GET: /Code/New

        [EzAuthorize]
        public ActionResult New()
        {
            return View();
        }

        //
        // POST: /Code/New

        [HttpPost]
        [EzAuthorize]
        [ValidateInput(false)]
        public ActionResult New(NewCodePostViewModel model, FormCollection formCollection)
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

                    var post = context.CodePosts.Create();
                    post.CodeTitle = WebHelper.StripTags(model.CodeTitle);
                    post.CodeContent = WebHelper.StripTags(model.CodeContent);
                    post.DatePosted = DateTime.Now;
                    post.Tags.AddRange(taglist);
                    context.CodePosts.Add(post);
                    context.SaveChanges();
                }

                return RedirectToAction("Manage", "Code");
            }

            return View(model);
        }

        //
        // GET: /Code/Edit

        [EzAuthorize]
        public ActionResult Edit(int id)
        {
            EditCodePostViewModel model = new EditCodePostViewModel();

            using (var context = new WebsiteContext())
            {
                if (!context.CodePosts.Any(x => x.CodePostId == id))
                    RedirectToAction("Manage", "Code");

                // eagerly load the tags as the context will be disposed
                var post = (from t in context.CodePosts.Include("Tags")
                            where t.CodePostId == id
                            select t).First();

                model.CodePostId = post.CodePostId;
                model.CodeTitle = post.CodeTitle;
                model.CodeContent = post.CodeContent;
                ViewBag.Tags = TagHelper.GetTagArray(post.Tags);
            }

            return View(model);
        }

        //
        // POST: /Code/Edit

        [HttpPost]
        [EzAuthorize]
        [ValidateInput(false)]
        public ActionResult Edit(EditCodePostViewModel model, FormCollection formCollection)
        {
            var tagcsv = "";
            if (!string.IsNullOrEmpty(formCollection["tags"]))
                tagcsv = formCollection["tags"];
            var tags = TagHelper.GetTagArray(tagcsv);

            if (ModelState.IsValid)
            {
                using (var context = new WebsiteContext())
                {
                    if (!context.CodePosts.Any(x => x.CodePostId == model.CodePostId))
                        return RedirectToAction("Manage", "Code");

                    // make sure tags exist in database, if not, they're added
                    TagHelper.AddTagRange(tags);

                    var taglist = new List<Tag>();
                    foreach (string tag in tags)
                    {
                        var tagObject = TagHelper.GetTag(context, tag);
                        taglist.Add(tagObject);
                    }

                    var post = context.CodePosts.First(x => x.CodePostId == model.CodePostId);
                    post.CodeTitle = WebHelper.StripTags(model.CodeTitle);
                    post.CodeContent = WebHelper.StripTags(model.CodeContent);
                    post.DateModified = DateTime.Now;
                    post.Tags.Clear();
                    post.Tags.AddRange(taglist);
                    context.SaveChanges();
                }

                return RedirectToAction("Manage", "Code");
            }

            ViewBag.Tags = tags;
            return View(model);
        }

        //
        // GET: /Code/Delete

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
                context.CodePosts.Delete(x => list.Contains(x.CodePostId));
            }

            return RedirectToAction("Manage", "Code");
        }
    }
}

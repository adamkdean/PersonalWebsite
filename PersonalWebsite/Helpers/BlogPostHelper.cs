using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace PersonalWebsite.Helpers
{
    public static class BlogPostHelper
    {        
        public static BlogPostsViewModel GetRecentBlogPosts(int limit = 5)
        {            
            var model = new BlogPostsViewModel();

            using (var context = new WebsiteContext())
            {
                // eagerly load the tags/comments etc as the context will be disposed
                var posts = (from t in context.BlogPosts
                                              .Include("Tags")
                                              .Include("Comments")
                             orderby t.DatePosted descending
                             select t).Take(limit).ToList();
                model.BlogPosts = posts;
            }

            return model;
        }
    }
}
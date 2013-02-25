using PersonalWebsite.Models;
using PersonalWebsite.Models.Blog;
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
        public static List<BlogPost> GetRecentBlogPosts(int limit = 5)
        {
            var list = new List<BlogPost>();

            using (var context = new WebsiteContext())
            {
                // eagerly load the tags/comments etc as the context will be disposed
                list = (from t in context.BlogPosts
                                         .Include("Tags")
                                         .Include("Comments")
                        orderby t.DatePosted descending
                        select t).Take(limit).ToList();                
            }

            return list;
        }
    }
}
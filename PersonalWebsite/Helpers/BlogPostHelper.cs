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
        public static List<BlogPost> GetRecentPosts(int limit = 5)
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

        public static List<BlogPost> GetAllPosts(bool loadAssets = true)
        {
            var list = new List<BlogPost>();

            using (var context = new WebsiteContext())
            {
                if (loadAssets)
                {
                    list = (from t in context.BlogPosts
                                             .Include("Tags")
                                             .Include("Comments")
                            orderby t.DatePosted descending
                            select t).ToList();
                }
                else
                {
                    list = (from t in context.BlogPosts
                            orderby t.DatePosted descending
                            select t).ToList();
                }
            }

            return list;
        }
    }
}
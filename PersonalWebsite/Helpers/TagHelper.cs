using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PersonalWebsite.Models;
using PersonalWebsite.Extensions;

namespace PersonalWebsite.Helpers
{
    public static class TagHelper
    {
        public static void AddTag(string tag)
        {
            using (var context = new WebsiteContext())
            {
                var tag_exists = context.Tags.Any(x => x.TagName.Equals(tag, StringComparison.OrdinalIgnoreCase));
                if (!tag_exists)
                {
                    var newtag = context.Tags.Create();
                    newtag.TagName = tag;
                    newtag.Slug = tag.Slugify();                    
                    context.Tags.Add(newtag);
                    context.SaveChanges();                    
                }
            }
        }

        public static void AddTagRange(string[] tags)
        {            
            foreach (string tag in tags) AddTag(tag);
        }

        public static Tag GetTag(string tag)
        {
            using (var context = new WebsiteContext())
            {
                if (tag == "") return null;
                return context.Tags.First(x => x.TagName.Equals(tag, StringComparison.OrdinalIgnoreCase));
            }
        }

        public static Tag GetTag(WebsiteContext context, string tag)
        {
            if (tag == "") return null;
            return context.Tags.First(x => x.TagName.Equals(tag, StringComparison.OrdinalIgnoreCase));
        }

        public static string[] GetTagArray(string list)
        {
            if (list == "") return new string[] { };
            return list.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] GetTagArray(List<Tag> tags)
        {
            if (tags == null) return new string[] { };
            List<string> list = new List<string>();
            foreach (var tag in tags)
                list.Add(tag.TagName);
            return list.ToArray();                
        }

        public static List<Tag> GetTagRange(string[] range)
        {
            List<Tag> list = new List<Tag>();
            foreach (var t in range)
            {
                Tag tag = GetTag(t);
                if (tag != null) list.Add(tag);
            }
            return list;
        }

        public static List<Tag> GetRandomTags(int limit = 0)
        {
            var tags = new List<Tag>();

            using (var context = new WebsiteContext())
            {
                var query = (from t in context.Tags 
                             where t.BlogPosts.Count() > 0
                             select t).Include("BlogPosts");

                if (limit == 0) tags = query.ToList();
                else tags = query.Take(limit).ToList();

                // // not to be confused with the Harlem Shake 
                // (https://www.youtube.com/watch?v=4hpEnLtqUDg)
                tags.Shuffle(); 
            }

            return tags;
        }

        public static List<Tag> GetTagsByMostPopular(int limit = 0)
        {
            var tags = new List<Tag>();

            using (var context = new WebsiteContext())
            {
                var query = (from t in context.Tags
                             where t.BlogPosts.Count() > 0
                             orderby t.BlogPosts.Count descending
                             select t).Include("BlogPosts");

                if (limit == 0) tags = query.ToList();
                else tags = query.Take(limit).ToList();
            }

            return tags;
        }
    }
}
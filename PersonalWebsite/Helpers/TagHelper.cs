using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
                    context.Tags.Add(newtag);
                    context.SaveChanges();                    
                }
            }
        }

        public static void AddTagRange(string[] tags)
        {            
            foreach (string tag in tags) AddTag(tag);
        }

        public static string[] GetTagsFromCSV(string list)
        {
            if (list == "") return new string[] { };
            return list.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static Tag GetTag(WebsiteContext context, string tag)
        {
            if (tag == "") return null;
            return context.Tags.First(x => x.TagName.Equals(tag, StringComparison.OrdinalIgnoreCase));
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
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
    }
}
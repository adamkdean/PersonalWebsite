using System.Collections.Generic;

namespace PersonalWebsite.Models.Blog
{
    public class TaggedViewModel
    {
        public string TagName { get; set; }
        public List<BlogPost> BlogPosts { get; set; }
    }
}
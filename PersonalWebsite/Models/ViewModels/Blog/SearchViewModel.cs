using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PersonalWebsite.Models.Blog
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public List<BlogPost> BlogPosts { get; set; }
    }
}
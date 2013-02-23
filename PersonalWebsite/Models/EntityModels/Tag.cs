using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }

        public virtual List<BlogPost> BlogPosts { get; set; }
        public virtual List<CodePost> CodePosts { get; set; }

        public Tag()
        {
            BlogPosts = new List<BlogPost>();
            CodePosts = new List<CodePost>();
        }        
    }
}
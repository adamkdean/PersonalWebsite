using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }

        public DateTime DatePosted { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual List<Tag> Tags { get; set; }
        public BlogPost()
        {
            Tags = new List<Tag>();
        }
    }
}
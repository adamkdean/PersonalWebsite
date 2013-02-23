using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class CodePost
    {
        public int CodePostId { get; set; }
        public string CodeTitle { get; set; }
        public string CodeContent { get; set; }

        public DateTime DatePosted { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual List<Tag> Tags { get; set; }
        
        public CodePost()
        {
            Tags = new List<Tag>();
        }
    }
}
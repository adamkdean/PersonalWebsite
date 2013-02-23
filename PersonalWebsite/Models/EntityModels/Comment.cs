using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class Comment
    {
        public int CommentId { get; set; }        
        public string CommentAuthor { get; set; }
        public string CommentContent { get; set; }

        public DateTime DatePosted { get; set; }
        public string IPAddress { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PersonalWebsite.Models.Blog
{
    public class EditViewModel
    {
        [Required]                
        public int BlogPostId { get; set; }

        [Required]
        [Display(Name = "Blog Title")]
        public string BlogTitle { get; set; }

        [Required]        
        [Display(Name = "Blog Content")]
        public string BlogContent { get; set; }
    }
}
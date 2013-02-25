using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PersonalWebsite.Models.Blog
{
    public class NewViewModel
    {
        [Required]        
        [Display(Name = "Blog Title")]
        public string BlogTitle { get; set; }

        [Required]
        [Display(Name = "Blog Content")]
        public string BlogContent { get; set; }        
    }
}
using System.ComponentModel.DataAnnotations;

namespace PersonalWebsite.Models
{
    public class NewBlogPostViewModel
    {
        [Required]
        public string BlogTitle { get; set; }
        [Required]
        public string BlogContent { get; set; }        
    }
}
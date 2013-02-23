using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PersonalWebsite.Models
{
    public class EditCodePostViewModel
    {
        [Required]
        public int CodePostId { get; set; }

        [Required]
        [Display(Name = "Code Title")]
        public string CodeTitle { get; set; }

        [Required]
        [Display(Name = "Code Content")]
        public string CodeContent { get; set; }
    }
}
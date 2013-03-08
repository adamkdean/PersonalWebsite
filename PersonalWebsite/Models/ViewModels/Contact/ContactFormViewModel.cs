using PersonalWebsite.Classes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalWebsite.Models.Contact
{
    public class ContactFormViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }

        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public ContactFormViewModel()
        {
            Success = false;
            ErrorMessage = "";
        }
    }
}
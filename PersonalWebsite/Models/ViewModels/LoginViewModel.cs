using System.ComponentModel.DataAnnotations;

namespace PersonalWebsite.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]        
        public string Password { get; set; }
    }
}
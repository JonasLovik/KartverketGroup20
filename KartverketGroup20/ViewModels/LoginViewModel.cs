using System.ComponentModel.DataAnnotations;

namespace KartverketGroup20.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passord")]

        public string Password { get; set; }
    }
}

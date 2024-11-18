using System.ComponentModel.DataAnnotations;

namespace KartverketGroup20.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Navn")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passord")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Bekreft passord")]

        public string ConfirmPassword { get; set; }


    }
}

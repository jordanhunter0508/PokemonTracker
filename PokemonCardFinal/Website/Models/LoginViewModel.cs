using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class SignupViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(250, ErrorMessage = "Email must be less than 250 characters.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name must be less than 50 characters.")]
        [Display(Name = "First Name")]
        public string GivenName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Last name must be less than 100 characters.")]
        [Display(Name = "Last Name")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "Password is too short.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Retype your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Retype Password")]
        [Compare("Password",ErrorMessage = "Passwords do not match.")]
        public string RetypePassword { get; set; }     
    }
}

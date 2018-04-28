using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression("([0-9]{6})", ErrorMessage = "The {0} must be a 6 figure number.")]
        [Display(Name = "Student Number")]
        public int StudentNumber { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Forename")]
        public string Forename { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [RegularExpression("([0-9]{6})", ErrorMessage = "The {0} must be a 6 figure number.")]
        [Display(Name = "Student Number")]
        public int StudentNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}

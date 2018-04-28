using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Forename { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int StudentNumber { get; set; }
        //public virtual ICollection<HelpRequest> Requests { get; set; }

    }
}

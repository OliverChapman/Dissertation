using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.LecturerViewModels
{
    public class StudentUserListViewModel
    {
        public string Id { get; set; }
        [DisplayName("Student Number")]
        public int StudentNumber { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        public string Surname { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.StudentViewModels
{
    public class HelpRequestViewModel
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}

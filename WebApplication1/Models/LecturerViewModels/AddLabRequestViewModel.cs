using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.LecturerViewModels
{
    public class AddLabRequestViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string ModuleName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string ModuleNo { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string RoomName { get; set; }
    }
}

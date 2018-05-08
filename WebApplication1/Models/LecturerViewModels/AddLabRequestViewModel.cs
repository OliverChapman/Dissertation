using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models.LecturerViewModels
{
    public class AddLabRequestViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Module Name")]
        public string ModuleName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Module No.")]
        public string ModuleNo { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Room Name")]
        public string RoomName { get; set; }

        public SelectList RoomNames { get; set; }
    }
}

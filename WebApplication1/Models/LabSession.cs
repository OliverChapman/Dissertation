using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class LabSession
    {
        [Key]
        public int LabId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleNo { get; set; }
        public string RoomName { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        
    }
}

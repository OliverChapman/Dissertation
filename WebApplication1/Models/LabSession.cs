using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class LabSession
    {
        public LabSession()
        {
            SessionComplete = false;
            DateCreated = DateTime.Now;

        }

        [Key]
        public int LabId { get; set; }

        [DisplayName("Lab Name")]
        public string ModuleName { get; set; }
        [DisplayName("Module Number")]
        public string ModuleNo { get; set; }
        [DisplayName("Room Name")]
        public string RoomName { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public bool SessionComplete { get; set; }
        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }
    }
}

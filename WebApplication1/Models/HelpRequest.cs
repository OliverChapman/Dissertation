using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class HelpRequest
    {
        [Key]
        public int Id { get; set; }
        public string HelpDesc { get; set; }
        //this should only ever have one student and one demonstrator attached at any time
        public virtual ICollection<UserToRequest> StudentAndDemoUsers { get; set; }
        public DateTime DateCreated { get; set; }
        public Status Status { get; set; }

        public HelpRequest()
        {
            DateCreated = DateTime.Now;
            Status = Status.Requested;
        }

    }

    public enum Status
    {
        Requested,
        InProgress,
        Completed,
        Suspended
    }
}

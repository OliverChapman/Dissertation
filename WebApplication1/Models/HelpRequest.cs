using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class HelpRequest
    {
        public HelpRequest()
        {
            RequestSent = DateTime.Now;
        }
        [Key]
        public int RequestId { get; set; }

        public DateTime RequestSent { get; set; }
        public bool Completed { get; set; }
        public string RequestInfo { get; set; }

    }
}

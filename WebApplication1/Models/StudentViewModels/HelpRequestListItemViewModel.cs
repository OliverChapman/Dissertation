using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.StudentViewModels
{
    public class HelpRequestListItemViewModel
    {
        [DisplayName("Problem")]
        public string Problem { get; set; }
        [DisplayName("Date Requested")]
        public DateTime DateRequested { get; set; }
        public Status Status { get; set; }

    }
}

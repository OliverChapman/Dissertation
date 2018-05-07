using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.DemonstratorViewModels
{
    public class HelpRequestListViewModel
    {
        [DataType(DataType.Text)]
        public string Location { get; set; }
        [DataType(DataType.Duration)]
        [DisplayName("Time Requested")]
        public string TimeRequested { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Help Request Details")]
        public string DescOfProblem { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Student Number")]
        public int StudentNumber { get; set; }
        public int Id { get; set; }

    }
}

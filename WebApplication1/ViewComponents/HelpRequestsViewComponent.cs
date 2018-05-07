using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DemonstratorViewModels;

namespace WebApplication1.ViewComponents
{
    public class HelpRequestsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public HelpRequestsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var helpList = _context.HelpRequest.Where(x => x.Status == Status.Requested).Include(x=>x.StudentAndDemoUsers).ThenInclude(x=>x.User);   
            var newList = new List<HelpRequestListViewModel>();
            foreach (var helpReq in helpList)
            {
                var listModel = new HelpRequestListViewModel
                {
                    DescOfProblem = helpReq.HelpDesc,
                    Id = helpReq.Id,
                    TimeRequested = helpReq.DateCreated.ToString(CultureInfo.InvariantCulture)
                };
                var firstOrDefault = helpReq.StudentAndDemoUsers.FirstOrDefault();
                if (firstOrDefault == null) continue;
                var user = firstOrDefault.User;
                listModel.Location = user.Location;
                listModel.StudentNumber = user.StudentNumber;
                newList.Add(listModel);
            }
            return View(newList.AsQueryable());
        }
    }
}

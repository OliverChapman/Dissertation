using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.LecturerViewModels;

namespace WebApplication1.ViewComponents
{
    public class AddLabSessionViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(AddLabRequestViewModel model)
        {
            //if(model != null)
            ModelState.Clear();
                return View(model);
            return View();
        }
    }
}

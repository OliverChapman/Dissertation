using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.Models.LecturerViewModels;

namespace WebApplication1.ViewComponents
{
    public class AddLabSessionViewComponent : ViewComponent
    {
        private readonly IHostingEnvironment _hostingEnviroment;

        public AddLabSessionViewComponent(IHostingEnvironment hostingEnviroment)
        {
            _hostingEnviroment = hostingEnviroment;
        }
        public async Task<IViewComponentResult> InvokeAsync(AddLabRequestViewModel model)
        {
            if (model == null)
                model = new AddLabRequestViewModel();
            ModelState.Clear();
            var selectList = new SelectList(GetLocation());
            model.RoomNames = selectList;
                return View(model);
            //return View();
        }
        private IEnumerable<string> GetLocation()
        {
            var jsonString = GetJsonString();
            var jsonObject = JsonConvert.DeserializeObject<LocationsClass>(jsonString);
            var result = jsonObject.Locations.Select(x => x.LocationName).ToList();
            return result;
        }
        private string GetJsonString()
        {
            var contentRoot = _hostingEnviroment.ContentRootPath;
            var result = System.IO.File.ReadAllText(contentRoot + "/Locations.json");
            return result;
        }
    }
}

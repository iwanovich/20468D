using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using WorldJourney.Models;

namespace WorldJourney.Controllers
{
    public class CityController : Controller
    {
        private IData _data;
        private IHostingEnvironment _environment;

        public CityController(IData data, IHostingEnvironment environment)
        {
            _data = data;
            _environment = environment;
            _data.CityInitializeData();
        }

        public IActionResult Index()
        {
            ViewData["Page"] = "Search city";
            return View();
        }

        public IActionResult Details(int? id)
        {
            ViewData["Page"] = "Selected city";
            City city = _data.GetCityById(id);
            if(city == null)
            {
                return NotFound();
            }
            ViewBag.Title = city.CityName;
            return View(city);
        }

        public IActionResult GetImage(int? CityId)
        {
            ViewData["Message"] = "Display Image";
            City requestedCity = _data.GetCityById(CityId);
            if(requestedCity != null)
            {
                string webRootPath = _environment.WebRootPath;
                string folderPath = "/images/";
                string fullPath = webRootPath + folderPath + requestedCity.ImageName;
                //string fullPath = Path.Combine(webRootPath, folderPath, requestedCity.ImageName);
                FileStream fileOnDisk = new FileStream(fullPath, FileMode.Open);
                byte[] fileBytes;
                using (BinaryReader br = new BinaryReader(fileOnDisk))
                {
                    fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                }
                return File(fileBytes, requestedCity.ImageMimeType);
            } else
            {
                return NotFound();
            }
        }
    }
}

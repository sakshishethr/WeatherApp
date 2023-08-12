using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Data;

namespace WebWeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Search()
        {
            var WeatherDataList = await Seed.PopulateByRandValues();
            return View(WeatherDataList);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string CityName)
        {
            if (CityName != null)  
            {
                TempData["CityName"] = CityName;
                return Redirect("/Weather/Details");
            }
            else
            {
                return await Search();
            }
       
        }

       [HttpGet]
        public async Task<ActionResult> Details()
        {
            try
            {
                var showWeather = new WeatherData();
                showWeather.name = TempData["CityName"].ToString();
                TempData.Remove("CityName");
                showWeather = await Seed.PopulateWeatherModel(showWeather.name);

                if (!showWeather.Equals(null))
                {
                    return View(showWeather);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
            catch(Exception)
            {
                return NotFound();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Data;
using WebWeatherApp.Models;

namespace WebWeatherApp
{
    public class Seed
    {
        private const int GrabDailyForeCastData = 4;
        private const int GrabWeeklyForeCastData = 5;
        private const int GrabWeatherData = 1;

        public static async Task<WeatherData> PopulateWeatherModel(string city)
        { 
            FetchData fetch = new FetchData();
            WeatherData weatherData = await fetch.GetAPIResponse(city,GrabWeatherData);
            return weatherData;
        }

        public static async Task<List<WeatherData>> PopulateByRandValues()
        {
            FetchData fetch = new FetchData();
            List<WeatherData> listWithWeather = new List<WeatherData>();
            for(int i = 0;i < 5; i++)
            {
                listWithWeather.Add(await fetch.GetRandomCity());
            }
            return listWithWeather;
        }
    }
}

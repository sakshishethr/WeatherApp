using System;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp.Data
{
    public class FetchData
    {
        #region url varibles
        private const string URL = "http://api.openweathermap.org/data/2.5/";
        private const string APIKEY = "&appid=199fefc6e88c9173d5f50323d8592652";
        private const string MetricUnits = "&units=metric";
        private const int GrabDailyForeCastData = 4;
        private const int GrabWeeklyForeCastData = 5;
        private const int GrabWeatherData = 1;
        #endregion

        private WeatherData weatherData = new WeatherData();
        #region creating url and connecting to client
        public async Task<WeatherData> GetAPIResponse(string userInput, int value)//grab data by city name
        {
            if (value.Equals(GrabWeatherData))
            {
                string weatherForCity = $"weather?q=+{userInput}";
                string path = URL + weatherForCity + MetricUnits + APIKEY;
                try
                {
                    weatherData = await ConnectToClient(path);
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }
            }
            else if (value.Equals(GrabDailyForeCastData) || 
                value.Equals(GrabWeeklyForeCastData))
            {
                string weatherForCity = $"forecast?q={userInput}";
                string path = URL + weatherForCity + MetricUnits + APIKEY;

                try
                {
                    weatherData = await ConnectToClient(path);
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }
            }
            return weatherData;
        }

        public async Task<WeatherData> GetAPIResponse(int lat, int lon)//grab data by coords
        {
            string weatherForCoord = $"weather?lat={lat}&lon={lon}";
            string path = URL + weatherForCoord + MetricUnits + APIKEY;
            weatherData = await ConnectToClient(path);
            return weatherData;
        }
        #endregion

        public async Task<WeatherData> GetRandomCity()
        {
            WeatherData weatherData = new WeatherData();
            Random rnd = new Random();
            int num = rnd.Next(1, 13);  // creates a number between 1 and 12
            string city = string.Empty;
            switch (num)
            {
                case 1:
                    return weatherData = await GetAPIResponse("Stockholm", GrabWeatherData);
                case 2:
                    return weatherData = await GetAPIResponse("New York", GrabWeatherData);
                case 3:
                    return weatherData = await GetAPIResponse("France",GrabWeatherData);
                case 4:
                    return weatherData = await GetAPIResponse("Mexico City", GrabWeatherData);
                case 5:
                    return weatherData = await GetAPIResponse("Havana", GrabWeatherData);
                case 6:
                    return weatherData = await GetAPIResponse("Abidjan", GrabWeatherData);
                case 7:
                    return weatherData = await GetAPIResponse("Madrid", GrabWeatherData);
                case 8:
                    return weatherData = await GetAPIResponse("China", GrabWeatherData);
                case 9:
                    return weatherData = await GetAPIResponse("Moscow", GrabWeatherData);
                case 10:
                    return weatherData = await GetAPIResponse("Oslo", GrabWeatherData);
                case 11:
                    return weatherData = await GetAPIResponse("Sydney", GrabWeatherData);
                case 12:
                    return weatherData = await GetAPIResponse("Baghdad", GrabWeatherData);
            }

            return null;
        }

        #region connecting to api and grabbing a response string
        private async Task<WeatherData> ConnectToClient(string path)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(path))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var response = await content.ReadAsStringAsync();
                            if (response.Contains("404").Equals(true))
                            {
                                throw new Exception("No data found, wrong input ?");
                            }
                            else
                            {
                                weatherData = HandleDataFormat.DeserializeJsonObject(response);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return weatherData;
        }
        #endregion
        

    }

}

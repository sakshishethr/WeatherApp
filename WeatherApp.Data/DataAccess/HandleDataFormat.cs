using Newtonsoft.Json;
using System;


namespace WeatherApp.Data
{
    public class HandleDataFormat
    {
        public static WeatherData DeserializeJsonObject(string response)
        {
            WeatherData weather = new WeatherData();
            weather = JsonConvert.DeserializeObject<WeatherData>(response);
            return weather;
        }
    }
}

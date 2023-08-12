using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Data;

namespace WeatherApp
{
    public class InPut
    {
        private static WeatherData weathers;
        private static FetchData data = new FetchData();
        private const int _SearchForOneCity = 1;
        private const int _SearchBylongLat = 2;
        private const int _SearchForMultipleCities = 3;
        private const int _FourDaysForeCast = 4;
        private const int _DailyForCast = 5;
        private const int menuX = 44;
        private const int menuY = 10;

        public static async Task<List<string>> InPutString(int i)
        {
            string input = string.Empty;
            List<string> apiResponse = new List<string>();

            if (i.Equals(_SearchForOneCity))
            {
                OutPut.PrintTitleFrame();
                OutPut.PrintMenuFrame();
                ColorAndStyle.SetTextColor(Colors.Magenta);
                input = EnterStringValue(ColorAndStyle.SetTextPosition("Enter city name: ",menuX,menuY));
                if (input.Equals(string.Empty))
                    return null;

                apiResponse = OutPut.PrintWeatherCondition(await data.GetAPIResponse(input, i)).ToList();
            }
            else if (i.Equals(_SearchBylongLat))
            {
                OutPut.PrintTitleFrame();
                OutPut.PrintMenuFrame();
                ColorAndStyle.SetTextColor(Colors.Magenta);
                int lon = int.Parse(EnterStringValue(ColorAndStyle.SetTextPosition("Enter lat value: ", menuX, menuY)));
                int lat = int.Parse(EnterStringValue(ColorAndStyle.SetTextPosition("Enter lon value: ", menuX, menuY)));
                Console.Clear();
                apiResponse = OutPut.PrintWeatherCondition(await data.GetAPIResponse(lat, lon)).ToList();
            }
            else if (i.Equals(_SearchForMultipleCities))
            {
                OutPut.PrintTitleFrame();
                OutPut.PrintMenuFrame();
                ColorAndStyle.SetTextColor(Colors.Magenta);
                List<string> apiData = new List<string>();
                foreach (var s in await AddCityNames(i))
                {
                    apiData = OutPut.PrintWeatherCondition(s).ToList();
                    if (!apiData.Contains("404"))
                        apiResponse.AddRange(apiData);
                }
            }
            else if (i.Equals(_FourDaysForeCast))
            {
                OutPut.PrintTitleFrame();
                OutPut.PrintMenuFrame();
                ColorAndStyle.SetTextColor(Colors.Magenta);
                apiResponse = OutPut.PrintFourDaysForecast(await data.GetAPIResponse(EnterStringValue(ColorAndStyle.SetTextPosition("Enter city name: ", menuX, menuY)), i));
            }
            else if (i.Equals(_DailyForCast))
            {
                OutPut.PrintTitleFrame();
                OutPut.PrintMenuFrame();
                ColorAndStyle.SetTextColor(Colors.Magenta);
                apiResponse = OutPut.PrintDailyForecast(await data.GetAPIResponse(EnterStringValue(ColorAndStyle.SetTextPosition("Enter city name: ", menuX, menuY)), i)).ToList();
            }

            return apiResponse;
        }

        private static string EnterStringValue(string output)
        {
            Console.Write(output);
            string value = string.Empty;
            value = Console.ReadLine();
            return value;
        }

        private static async Task<List<WeatherData>> AddCityNames(int value)
        {
            List<WeatherData> listOfWeatherData = new List<WeatherData>();
            bool continueLoop = true;
            string input = string.Empty;

            while (continueLoop.Equals(true))
            {
                #region menu prompts
                if (listOfWeatherData.Count < 1)
                {
                    ColorAndStyle.SetTextColor(Colors.red);
                    Console.Write(ColorAndStyle.SetTextPosition("Press enter to exit or enter city", menuX-7, menuY));
                    ColorAndStyle.SetTextColor(Colors.Magenta);
                    Console.WriteLine(ColorAndStyle.SetTextPosition("Enter city  name:",menuX-7,menuY+1));
                    ColorAndStyle.SetTextPosition(string.Empty, menuX + 11, menuY + 1);
                    input = Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Threads.StartThreadWithJoin(new Thread(new ThreadStart(OutPut.PrintTitleFrame)));
                    Threads.StartThreadWithJoin(new Thread(new ThreadStart(OutPut.PrintMenuFrame)));
                    ColorAndStyle.SetTextColor(Colors.red);
                    Console.Write(ColorAndStyle.SetTextPosition("Press enter to exit or enter city", menuX-7, menuY));
                    ColorAndStyle.SetTextColor(Colors.Magenta);
                    Console.WriteLine(ColorAndStyle.SetTextPosition("Enter another city: ", menuX-7, menuY + 1));
                    ColorAndStyle.SetTextPosition(string.Empty, menuX + 13, menuY + 1);
                    input = Console.ReadLine();
                }
                #endregion

                #region handle user input
                if (!input.Equals(string.Empty))
                {
                    weathers = await data.GetAPIResponse(input, 1);
                    listOfWeatherData.Add(weathers);
                }
                else if (listOfWeatherData.Count < 1 && input.Equals(string.Empty))
                {
                    throw new Exception("first input cant be empty!!");
                }
                else
                {
                    continueLoop = false;
                }
                #endregion
            }

            return listOfWeatherData;
        }


    }
}

using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Data;
namespace WeatherApp
{
    public class OutPut
    {
        private const int menuY = 8;
        private const int menuX = 36;
        private const int MenuSize = 7;

        private static string PrintMenuOptions(int index,int x,int y)
        {
            string [] menuText = {"Search For City Press 1:", "Search by lon and lat Press 2:", "Collect multiple city data Press 3:", 
                "Four Days Weather Forecast Press 4:", "Daily Weather Forecast 5:" ,"Close Program Press 6:", ":> "};
            ColorAndStyle.SetTextColor(Colors.Magenta);
            return ColorAndStyle.SetTextPosition(menuText[index], x, y + index);
        }

        public static string PrintErrorMessages(string message,int userX,int userY)
        {
            Console.Clear();
            Threads.StartThreadWithJoin(new Thread(new ThreadStart(PrintTitleFrame)));
            Threads.StartThread(new Thread(new ThreadStart(PrintMenuFrame)));

            ColorAndStyle.SetTextColor(Colors.red, string.Empty);
            return ColorAndStyle.SetTextPosition(message, menuX+userX, menuY+userY);
        }

        public static void PrintTitleFrame()
        {
            ColorAndStyle.SetTextColor(Colors.white);
            Console.WriteLine(ColorAndStyle.SetTextPosition("***********************************", menuX, menuY - 5));
            ColorAndStyle.SetTextColor(Colors.green);
            Console.WriteLine(ColorAndStyle.SetTextPosition("WeatherApp", menuX + 12, menuY - 4));
            ColorAndStyle.SetTextColor(Colors.white);

            for (int i = 0; i < MenuSize - 5; i++)
            {
                Console.WriteLine(ColorAndStyle.SetTextPosition("*", menuX, menuY + i - 4));
                Console.WriteLine(ColorAndStyle.SetTextPosition("*", menuX + 34, menuY + i - 4));
            }

        }

        public static void PrintMenuFrame()//TODO work in progress, just temporary. Want to avoid void
        {
            ColorAndStyle.SetTextColor(Colors.white);
            Console.WriteLine(ColorAndStyle.SetTextPosition("**************************************************", menuX - 8, menuY - 2));
            Console.WriteLine(ColorAndStyle.SetTextPosition("*", menuX + 41, menuY - 1));
            Console.WriteLine(ColorAndStyle.SetTextPosition("*", menuX + -8, menuY - 1));

            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(ColorAndStyle.SetTextPosition("*", menuX - 8, menuY + i));
                Console.WriteLine(ColorAndStyle.SetTextPosition("*", menuX + 41, menuY + i));
            }


            Console.WriteLine(ColorAndStyle.SetTextPosition("*", menuX - 8, menuY + MenuSize));
            Console.WriteLine(ColorAndStyle.SetTextPosition("*", menuX +41, menuY + MenuSize));
            Console.WriteLine(ColorAndStyle.SetTextPosition("**************************************************", menuX - 8, menuY + 13 + 1));
        }

        public static void PrintMainMenu()//TODO work in progress, just temporary. Want to avoid void
        {
            for (int i = 0; i < MenuSize; i++)
            {
                Console.Write(PrintMenuOptions(i, menuX, menuY+3));
            }
            ColorAndStyle.SetTextPosition("", menuX + 3, menuY + MenuSize+2);//resets the cursour at >: in the menu
        }

        public static string[] PrintWeatherCondition(WeatherData weather)
        {
            string [] data;
            if (!weather.Equals(null))
            {
                data = new string []{"********************************","* City: " + weather.name, "* Temperature: "+weather.main.temp, "* Highest temperature: " +weather.main.temp_max,"* Lowest temperatur: "+ weather.main.temp_min,
                "* Feels like: " + weather.main.feels_like,"* Humidity: "+ weather.main.humidity,"* Pressure: "+ weather.main.pressure,"* Windspeed: "+weather.Wind.speed,"* Deg: "+ weather.Wind.deg,
                "* Condition: "+ weather.weather[0].main,"* Description: " +weather.weather[0].description,"********************************"};
            }
            else
            {
                throw new Exception("PrintWeatherCondition cant be empty");
            }

            return data;
        }
        public static List<string> PrintFourDaysForecast(WeatherData weather)
        {
            int count = 0;
            List<string> data = new List<string>();
            data.Add("********************************");
            data.Add("* "+weather.city.name+" fourdays forecast");
            data.Add("* Lon: " + weather.city.coord.lon.ToString() + " Lat: " + weather.city.coord.lat.ToString());


            foreach (var s in weather.list)
            {
                count++;
                data.Add("********************************");
                data.Add("* Date: " + s.dt_txt);
                data.Add("* Temperatur: " + s.main.temp);
                data.Add("* Highest temperature: " + s.main.temp_max);
                data.Add("* Lowest temperature: " + s.main.temp_min);
                data.Add("* Feels like: " + s.main.feels_like);
                data.Add("* Humidity: " + s.main.humidity);
                data.Add("* Pressure: " + s.main.pressure);
                data.Add("* Windspeed: " + s.wind.speed);
                data.Add("* Condition: " + s.weather[0].main);
                data.Add("* Description: " + s.weather[0].description);
            }
            if (count.Equals(0))
            {
                data.Add("********************************");
                data.Add("* No data found!");
                data.Add("* Try Later or another city! ");
            }
            data.Add("********************************");
            return data;
        }

        public static List<string> PrintDailyForecast(WeatherData weather)
        {
            int count = 0;
            string dateNow = DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "-");
            List<string> data = new List<string>();
            data.Add("********************************");
            data.Add("* City: "+weather.city.name);
            data.Add("* Lon: " + weather.city.coord.lon.ToString() + " Lat: " + weather.city.coord.lat.ToString());
            foreach (var s in weather.list)
            {
                string Apidate = s.dt_txt.Remove(10).ToString();//removes time from the string

                if (Apidate.Equals(dateNow))
                {
                    count++;
                    data.Add("********************************");
                    data.Add("* Date: " + s.dt_txt);
                    data.Add("* Temperatur: " + s.main.temp);
                    data.Add("* Highest temperature: " + s.main.temp_max);
                    data.Add("* Lowest temperature: " + s.main.temp_min);
                    data.Add("* Feels like: " + s.main.feels_like);
                    data.Add("* Humidity: " + s.main.humidity);
                    data.Add("* Pressure: " + s.main.pressure);
                    data.Add("* Windspeed: " + s.wind.speed);
                    data.Add("* Condition: " + s.weather[0].main);
                    data.Add("* Descripton: " + s.weather[0].description);             
                }
            }
            if (count.Equals(0))
            {
                data.Add("********************************");
                data.Add("* No data found!");
                data.Add("* Try Later or another city! ");
            }
            data.Add("********************************");
            return data;
        }
    }
}

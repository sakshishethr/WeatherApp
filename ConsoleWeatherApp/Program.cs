using System;
using System.Threading.Tasks;

namespace WeatherApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.Title = "WeatherApp";
            await Menu.RunProgram();
        }
    }
}

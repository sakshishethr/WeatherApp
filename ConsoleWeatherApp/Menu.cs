using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Threading;

namespace WeatherApp
{
    public class Menu
    {
        private static bool willContinue = true;
        private static FetchData data = new FetchData();
        private const int ExitProgram = 6;

        public static async Task RunProgram()
        {

            while (willContinue.Equals(true))
            {
                Threads.StartThreadWithJoin(new Thread(new ThreadStart(OutPut.PrintTitleFrame)));
                Threads.StartThreadWithJoin(new Thread(new ThreadStart(OutPut.PrintMenuFrame)));
                Threads.StartThreadWithJoin(new Thread(new ThreadStart(OutPut.PrintMainMenu)));
                try
                {
                    willContinue = await MainMenuNavigation(int.Parse(Console.ReadLine()));// if false program will close
                }
                catch (FormatException)
                {
                   
                    Console.WriteLine(OutPut.PrintErrorMessages("Input cant be empty or in wrong format!!",-2,2));
                }
                catch (Exception e)
                {
                    Console.WriteLine(OutPut.PrintErrorMessages(e.Message,5,2));
                }
                finally
                {
                    if (willContinue.Equals(true))
                        Console.ReadKey();

                    Console.Clear();
                }

            }

        }

        private static async Task<bool> MainMenuNavigation(int userInput)
        {
            if (!userInput.Equals(ExitProgram))
            {
                string url = string.Empty;
                List<string>output;

                Console.Clear();
                output = await InPut.InPutString(userInput);

                if (output.Equals(null))
                {
                    throw new Exception("input cant be empty!!!");
                }
                else
                {
                    Console.Clear();
                    int count = 0;
                    ColorAndStyle.SetTextColor(Colors.Magenta);
                    for (int i = 0; i < output.Count(); i++)
                    {
                        Console.WriteLine(ColorAndStyle.SetTextPosition(output[i], 42, 9 + i));
                        Console.Write(ColorAndStyle.SetTextPosition("*",74,9+i));
                    }
                    willContinue = true;
                }
            }
            else
            {
                willContinue = false;
            }
            return willContinue;
        }
    }
}

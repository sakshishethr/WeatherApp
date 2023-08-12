using NUnit.Framework;
using System;
using System.Collections.Generic;
using WebWeatherApp;
using WeatherApp.Data;
 
namespace WeatherApp.Tests
{
    [TestFixture]
    public class OutputTests
    {


        [Test]
        public void PrintWeatherCondition_Should_Return_Valid_Output()
        {
            // Arrange
            var weather = new WeatherData
            {
                name = "City",
                main = new Main
                {
                    temp = 20,
                    temp_max = 25,
                    temp_min = 15,
                    feels_like = 22,
                    humidity = 70,
                    pressure = 1012
                },
                Wind = new Wind
                {
                    speed = 10,
                    deg = 180
                },
                weather = new List<Weather> { new Weather { main = "Clear", description = "Clear sky" } }
            };

            // Act
            string[] output = OutPut.PrintWeatherCondition(weather);

            // Assert
            Assert.AreEqual(13, output.Length);
            Assert.IsTrue(output[1].Contains("City"));
            Assert.IsTrue(output[2].Contains("20"));
            Assert.IsTrue(output[3].Contains("25"));
            Assert.IsTrue(output[4].Contains("15"));
            Assert.IsTrue(output[5].Contains("22"));
            Assert.IsTrue(output[6].Contains("70"));
            Assert.IsTrue(output[7].Contains("1012"));
            Assert.IsTrue(output[8].Contains("10"));
            Assert.IsTrue(output[9].Contains("180"));
            Assert.IsTrue(output[10].Contains("Clear"));
            Assert.IsTrue(output[11].Contains("Clear sky"));
        }

    }
}

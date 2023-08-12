using NUnit.Framework;
using Newtonsoft.Json.Linq;
using WeatherApp.Data;

namespace WeatherApp.Tests
{
    [TestFixture]
    public class WeatherDataTests
    {

        [Test]
        public void WeatherData_RequiredFields()
        {
            // Arrange
            var weatherData = new WeatherData();

            // Act
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(weatherData, null, null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(weatherData, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Any(result => result.MemberNames.Contains("name")));
        }
    }

    [TestFixture]
    public class WindTests
    {
        [Test]
        public void Wind_ToString()
        {
            // Arrange
            var wind = new Wind { speed = 10.5, deg = 180 };

            // Act
            var result = wind.ToString();

            // Assert
            Assert.AreEqual("speed: 10.5 deg 180", result);
        }
    }

}

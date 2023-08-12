using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using WebWeatherApp.Controllers;
using WeatherApp.Data;

namespace WebWeatherApp.Tests.Controllers
{
    [TestFixture]
    public class WeatherControllerTests
    {
        [Test]
        public async Task Search_Get_ReturnsViewWithModel()
        {
            // Arrange
            var controller = new WeatherController();

            // Act
            var result = await controller.Search() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(List<WeatherData>), result.Model.GetType());
        }

        [Test]
        public async Task Search_Post_WithCityName_RedirectsToDetails()
        {
            // Arrange
            var controller = new WeatherController();
            var cityName = "SampleCity"; // Replace with a valid city name

            // Act
            var result = await controller.Search(cityName) as RedirectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("/Weather/Details", result.Url);
        }

        [Test]
        public async Task Search_Post_WithoutCityName_RedirectsToSearch()
        {
            // Arrange
            var controller = new WeatherController();

            // Act
            var result = await controller.Search(null) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Search", result.ActionName);
        }

        [Test]
        public async Task Details_WithValidCityName_ReturnsViewWithModel()
        {
            // Arrange
            var controller = new WeatherController();
            var cityName = "SampleCity"; // Replace with a valid city name

            // Act
            var result = await controller.Details() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(WeatherData), result.Model.GetType());
        }

        [Test]
        public async Task Details_WithInvalidCityName_ReturnsNotFound()
        {
            // Arrange
            var controller = new WeatherController();

            // Act
            var result = await controller.Details() as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}

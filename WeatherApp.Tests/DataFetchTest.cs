using NUnit.Framework;
using WeatherApp.Data;
using System.Threading.Tasks;

namespace WeatherApp.Tests
{
    [TestFixture]
    public class FetchDataTests
    {
        private FetchData _fetchData;

        [SetUp]
        public void Setup()
        {
            _fetchData = new FetchData();
        }

        [Test]
        public async Task TestGetAPIResponseByCity()
        {
            WeatherData weatherData = await _fetchData.GetAPIResponse("London", 1); // Assuming 1 corresponds to GrabWeatherData

            Assert.IsNotNull(weatherData);
            Assert.AreEqual("London", weatherData.name);
        }

        [Test]
        public async Task TestGetRandomCity()
        {
            WeatherData weatherData = await _fetchData.GetRandomCity();

            Assert.IsNotNull(weatherData);
            Assert.IsNotNull(weatherData.name);
        }


        [Test]
        public async Task TestConnectToClientWithValidPath()
        {
            // This is a unit test, so you might want to mock HttpClient to avoid actual network calls.
            // However, for simplicity, let's assume a valid response here.

            WeatherData weatherData = await _fetchData.GetAPIResponse("London", 1);

            Assert.IsNotNull(weatherData);
            Assert.AreEqual("London", weatherData.name);
        }

    }


}

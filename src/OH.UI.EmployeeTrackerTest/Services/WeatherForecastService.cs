using Microsoft.Identity.Web;
using Newtonsoft.Json;
using OH.Common.EmployeeTrackerTest.Models;
using OH.Common.EmployeeTrackerTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OH.UI.EmployeeTrackerTest.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherForecastService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(List<WeatherForecastViewModel> WeatherForecastList, string ErrorMessage)> GetRandomWeatherForecastAsync()
        {
            var client = _httpClientFactory.CreateClient("EmployeeTrackerTestApi");

            const string endPointAddress = "WeatherForecast/GetRandomWeatherForecast";

            var response = await client.GetAsync(endPointAddress).ConfigureAwait(true);

            if (!response.IsSuccessStatusCode)
            {
                return (new List<WeatherForecastViewModel>(), response.ReasonPhrase);
            }

            var responseJsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

            return (JsonConvert.DeserializeObject<List<WeatherForecastViewModel>>(responseJsonString), string.Empty);
        }

        public async Task<(List<WeatherForecastViewModel> WeatherForecastList, string ErrorMessage)> GetWeatherForecastAsync()
        {
            var client = _httpClientFactory.CreateClient("EmployeeTrackerTestApi");

            const string endPointAddress = "WeatherForecast/GetWeatherForecast";

            var response = await client.GetAsync(endPointAddress).ConfigureAwait(true);
            
            if (!response.IsSuccessStatusCode)
            {
                return (new List<WeatherForecastViewModel>(), response.ReasonPhrase);
            }

            var responseJsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

            return (JsonConvert.DeserializeObject<List<WeatherForecastViewModel>>(responseJsonString), string.Empty);
        }
    }
}

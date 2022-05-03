using Microsoft.Identity.Web;
using Newtonsoft.Json;
using OH.Common.EmployeeTrackerTest.Models;
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
        private HttpClient client = new HttpClient();

        public WeatherForecastService()
        {
         
        }

        public async Task<(List<WeatherForecast> WeatherForecastList, string ErrorMessage)> GetWeatherForecastsync()
        {
            const string endPointAddress = "https://localhost:5002/WeatherForecast";

            var response = await client.GetAsync(endPointAddress).ConfigureAwait(true);


              


            if (!response.IsSuccessStatusCode)
            {
                return (new List<WeatherForecast>(), response.ReasonPhrase);
            }
            var responseJsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            return (JsonConvert.DeserializeObject<List<WeatherForecast>>(responseJsonString), string.Empty);

        }
    }
}

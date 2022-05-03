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
        private readonly IDownstreamWebApi _downstreamWebApi;

        public WeatherForecastService(IDownstreamWebApi downstreamWebApi)
        {
            _downstreamWebApi = downstreamWebApi;
        }

        public async Task<(List<WeatherForecast> WeatherForecastList, string ErrorMessage)> GetWeatherForecastsync()
        {
            const string endPointAddress = "WeatherForecast";

            var response = await _downstreamWebApi.CallWebApiForAppAsync(
                "ServiceLineManager",
                options =>
                {
                    options.HttpMethod = HttpMethod.Get;
                    options.RelativePath = $"{endPointAddress}";
                }).ConfigureAwait(true);


            if (!response.IsSuccessStatusCode)
            {
                return (new List<WeatherForecast>(), response.ReasonPhrase);
            }
            var responseJsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            return (JsonConvert.DeserializeObject<List<WeatherForecast>>(responseJsonString), string.Empty);

        }
    }
}

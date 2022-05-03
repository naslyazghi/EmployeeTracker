using OH.Common.EmployeeTrackerTest.Models;
using OH.UI.EmployeeTrackerTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OH.UI.EmployeeTrackerTest.Helper
{
    public class WeatherForecastHelper : IWeatherForecastHelper
    {
        protected readonly IWeatherForecastService WeatherForecastService;

        public WeatherForecastHelper(IWeatherForecastService weatherForecastService)
        {
            WeatherForecastService = weatherForecastService;
        }
        public async Task<List<WeatherForecast>> GetWeatherForecastsync()
        {
            var (weatherlist, errorMessage) = await WeatherForecastService.GetWeatherForecastsync().ConfigureAwait(true);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return (new List<WeatherForecast>());
            }
            else
            {
                return weatherlist;
            }
        }
    }
}

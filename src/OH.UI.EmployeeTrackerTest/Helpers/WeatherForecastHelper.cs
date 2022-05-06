using OH.Common.EmployeeTrackerTest.Models;
using OH.Common.EmployeeTrackerTest.ViewModels;
using OH.UI.EmployeeTrackerTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OH.UI.EmployeeTrackerTest.Helpers
{
    public class WeatherForecastHelper : IWeatherForecastHelper
    {
        protected readonly IWeatherForecastService WeatherForecastService;

        public WeatherForecastHelper(IWeatherForecastService weatherForecastService)
        {
            WeatherForecastService = weatherForecastService;
        }

        public async Task<(List<WeatherForecastViewModel> WeatherForecastList, string ErrorMessage)> GetRandomWeatherForecastAsync()
        {
            return await WeatherForecastService.GetRandomWeatherForecastAsync();
        }

        public async Task<(List<WeatherForecastViewModel> WeatherForecastList, string ErrorMessage)> GetWeatherForecastAsync()
        {
            return await WeatherForecastService.GetWeatherForecastAsync().ConfigureAwait(true);
        }
    }
}

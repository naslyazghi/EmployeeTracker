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
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastHelper(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        public async Task<(List<WeatherForecastViewModel> WeatherForecastList, string ErrorMessage)> GetRandomWeatherForecastAsync()
        {
            return await _weatherForecastService.GetRandomWeatherForecastAsync();
        }

        public async Task<(List<WeatherForecastViewModel> WeatherForecastList, string ErrorMessage)> GetWeatherForecastAsync()
        {
            return await _weatherForecastService.GetWeatherForecastAsync().ConfigureAwait(true);
        }
    }
}

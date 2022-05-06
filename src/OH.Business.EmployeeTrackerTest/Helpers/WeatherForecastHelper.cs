using OH.Business.EmployeeTrackerTest.Services;
using OH.Common.EmployeeTrackerTest.Models;
using OH.Common.EmployeeTrackerTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OH.Business.EmployeeTrackerTest.Helpers
{
    public class WeatherForecastHelper : IWeatherForecastHelper
    {
        readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastHelper(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        public async Task<IEnumerable<WeatherForecastViewModel>> GetWeatherForecastAsync()
        {
            return (await _weatherForecastService.GetWeatherForecastsync())
                .Select(e => new WeatherForecastViewModel() 
                { 
                    Date = DateTime.Parse(e.Date),
                    Summary = e.Summary,    
                    TemperatureC = e.TemperatureC
                });
        }
    }
}

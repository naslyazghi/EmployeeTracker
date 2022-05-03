using OH.Common.EmployeeTrackerTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OH.UI.EmployeeTrackerTest.Services
{
    public interface IWeatherForecastService
    {
        Task<(List<WeatherForecast> WeatherForecastList, string ErrorMessage)> GetWeatherForecastsync();
    }
}

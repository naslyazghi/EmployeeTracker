using OH.Common.EmployeeTrackerTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OH.UI.EmployeeTrackerTest.Helpers
{
    public interface IWeatherForecastHelper
    {
        Task<(List<WeatherForecastViewModel> WeatherForecastList, string ErrorMessage)> GetWeatherForecastAsync();
        Task<(List<WeatherForecastViewModel> WeatherForecastList, string ErrorMessage)> GetRandomWeatherForecastAsync();
    }

}

using OH.Common.EmployeeTrackerTest.Models;
using OH.Common.EmployeeTrackerTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OH.Business.EmployeeTrackerTest.Helpers
{
    public interface IWeatherForecastHelper
    {
        Task<IEnumerable<WeatherForecastViewModel>> GetWeatherForecastAsync();
    }
}

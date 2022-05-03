//TODO change
using OH.Common.EmployeeTrackerTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OH.UI.EmployeeTrackerTest.Helper
{
        public interface IWeatherForecastHelper
        {
            Task<List<WeatherForecast>> GetWeatherForecastsync();
        }

}

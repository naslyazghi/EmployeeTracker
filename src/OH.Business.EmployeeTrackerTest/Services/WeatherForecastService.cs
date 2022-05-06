using OH.Business.EmployeeTrackerTest.Data;
using OH.Common.EmployeeTrackerTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OH.Business.EmployeeTrackerTest.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        readonly EmployeeTrackerTestDbContext _context;

        public WeatherForecastService(EmployeeTrackerTestDbContext context)
        {
            _context = context;
        }
                
        //TODO: -- OH Implementation Note -- See notes about EFCore known async limitations with SQLite
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsync()
        {
            var list = _context.WeatherForecast.ToList();

            return await Task.FromResult(list);
        }
    }
}

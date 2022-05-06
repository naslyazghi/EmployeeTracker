using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OH.Business.EmployeeTrackerTest.Helpers;
using OH.Common.EmployeeTrackerTest.Models;
using OH.Common.EmployeeTrackerTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OH.Service.EmployeeTrackerTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastHelper _weatherForecastHelper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastHelper weatherForecastHelper)
        {
            _logger = logger;
            _weatherForecastHelper = weatherForecastHelper;
        }

        [HttpGet]
        [Route("GetRandomWeatherForecast")]
        public IEnumerable<WeatherForecastViewModel> GetRandomWeatherForecast()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastViewModel
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToList();
        }

        [HttpGet]
        [Route("GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecastViewModel>> GetWeatherForecast()
        {
            return await _weatherForecastHelper.GetWeatherForecastAsync();
        }
    }
}

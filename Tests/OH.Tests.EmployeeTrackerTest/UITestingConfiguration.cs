using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OH.Business.EmployeeTrackerTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using OH.Business.EmployeeTrackerTest.Helpers;
using OH.Business.EmployeeTrackerTest.Services;
using SQLitePCL;
using IWeatherForecastHelper = OH.UI.EmployeeTrackerTest.Helpers.IWeatherForecastHelper;
using IWeatherForecastService = OH.UI.EmployeeTrackerTest.Services.IWeatherForecastService;
using WeatherForecastHelper = OH.UI.EmployeeTrackerTest.Helpers.WeatherForecastHelper;
using WeatherForecastService = OH.UI.EmployeeTrackerTest.Services.WeatherForecastService;

namespace OH.Tests.EmployeeTrackerTest
{
    public class UITestingConfiguration
    {
        public bool RunIntegrationTest()
        {
            return true;
        }
        
        public ServiceProvider CreateConfigurationForTest()
        {
            
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient("EmployeeTrackerTestApi", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://localhost:5002/api/");
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "EmployeeTrackerTest");
            });

            services.AddScoped<IWeatherForecastHelper, WeatherForecastHelper>();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();

            return services.BuildServiceProvider();
        }
    }
}

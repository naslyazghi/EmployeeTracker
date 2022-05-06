using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OH.Business.EmployeeTrackerTest.Data;
using Microsoft.EntityFrameworkCore;
using OH.Business.EmployeeTrackerTest.Helpers;
using OH.Business.EmployeeTrackerTest.Services;
using SQLitePCL;

namespace OH.Tests.EmployeeTrackerTest
{
    public class ServicesTestingConfiguration
    {
        public bool RunIntegrationTest()
        {
            return true;
        }
        
        public ServiceProvider CreateConfigurationForTest()
        {
            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();

            var contentRootPath = app.Environment.ContentRootPath;
            var dbRelativeLocalPath = $"{contentRootPath}\\..\\..\\..\\..\\..\\database\\EmployeeTrackerTest.db";
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<EmployeeTrackerTestDbContext>(options => options.UseSqlite($"DataSource={dbRelativeLocalPath}"))
                .AddScoped<IWeatherForecastHelper, WeatherForecastHelper>()
                .AddScoped<IWeatherForecastService, WeatherForecastService>()
                ;

            raw.SetProvider(new SQLite3Provider_e_sqlite3());
            services.AddControllers();
            
            return services.BuildServiceProvider();
        }
    }
}

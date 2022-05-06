using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Web;
using OH.Business.EmployeeTrackerTest.Data;
using Microsoft.EntityFrameworkCore;
using OH.Business.EmployeeTrackerTest.Helpers;
using OH.Business.EmployeeTrackerTest.Services;

namespace OH.Service.EmployeeTrackerTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        //TODO: -- OH Implementation Note -- Hardcoded local path for simplicity.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbRelativeLocalPath = $"{WebHostEnvironment.ContentRootPath}\\..\\..\\database\\EmployeeTrackerTest.db";

            services.AddDbContext<EmployeeTrackerTestDbContext>(options => options.UseSqlite($"DataSource={dbRelativeLocalPath}"))
                .AddScoped<IWeatherForecastHelper, WeatherForecastHelper>()
                .AddScoped<IWeatherForecastService, WeatherForecastService>()
                ;

            services.AddControllers();
      
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

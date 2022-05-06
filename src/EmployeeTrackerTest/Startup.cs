using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Identity.Web;
using Blazored.LocalStorage;
using Blazored.Toast;
using OH.UI.EmployeeTrackerTest.Helpers;
using OH.UI.EmployeeTrackerTest.Services;
using Microsoft.Net.Http.Headers;

namespace EmployeeTrackerTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpClient("EmployeeTrackerTestApi", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://localhost:5002/api/");
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "EmployeeTrackerTest");
            });

            services.AddScoped<IWeatherForecastHelper, WeatherForecastHelper>();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddRazorPages();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredToast();
            services.AddAuthentication();
            services.AddServerSideBlazor()
                 .AddCircuitOptions(o =>
                 {
                     o.DetailedErrors = true;
                 })
                .AddMicrosoftIdentityConsentHandler();

            services.AddSingleton<WeatherForecastService>();

          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}

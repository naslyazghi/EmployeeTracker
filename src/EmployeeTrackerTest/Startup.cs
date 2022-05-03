using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.ApplicationInsights.Extensibility;
using System.IO;
using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Http;
using OH.UI.EmployeeTrackerTest.Helper;
using OH.UI.EmployeeTrackerTest.Services;

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

            //Add user auth for end users.  This uses auth code flow   https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-oauth2-auth-code-flow
            services.AddMicrosoftIdentityWebAppAuthentication(Configuration)
                .EnableTokenAcquisitionToCallDownstreamApi()
                .AddDownstreamWebApi(Program.AzureConfigApplicationName, options =>
                {
                    options.BaseUrl = Configuration["Global.APIUrl." + Program.AzureConfigApplicationName];
                    //TODO update the Scope after template generation
                    options.Scopes = "https://api.orhs.org/EmployeeTrackerTest/.default";
                })
                .AddInMemoryTokenCaches();


            services.AddScoped<IWeatherForecastHelper, WeatherForecastHelper>();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();


            services.AddRazorPages()
                    .AddMicrosoftIdentityUI();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredToast();
            services.AddAuthentication();
            services.AddServerSideBlazor()
                 .AddCircuitOptions(o =>
                 {
                     if (System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" || System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "DevRegion")
                     {
                         o.DetailedErrors = true;
                     }
                     o.DetailedErrors = true;
                 })
                .AddMicrosoftIdentityConsentHandler();

            services.AddSingleton<WeatherForecastService>();

            services.AddSingleton<ITelemetryInitializer, MyTelemetryInitializer>();
            services.AddApplicationInsightsTelemetry(Configuration["Global.Settings.AppInsightsKey"]);

            //ShowPII disabled by default.  Enable to show PII in the console to help troubleshoot authentication
            //IdentityModelEventSource.ShowPII = true;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //ShowPII disabled by default.  Enable to show PII in the console to help troubleshoot authentication
            //IdentityModelEventSource.ShowPII = true;

            app.UseAuthentication();

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

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/robots.txt"))
                {
                    var robotsTxtPath = Path.Combine(env.ContentRootPath, "robots.txt");
                    string output = "User-agent: *  \nDisallow: /";
                    if (File.Exists(robotsTxtPath))
                    {
                        output = await File.ReadAllTextAsync(robotsTxtPath);
                    }
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(output);
                }
                else await next();
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}

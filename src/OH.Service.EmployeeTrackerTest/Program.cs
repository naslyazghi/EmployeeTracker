using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OH.Service.EmployeeTrackerTest
{
    public class Program
    {

        //The ApplicationName must be unique to this application.  It is referenced several times below to grab settings from AppConfiguration
        //TODO Update AzureConfigApplicationName with correct application name
        public const string AzureConfigApplicationName = "OH.Service.EmployeeTrackerTest";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var settings = config.Build();
                    //Dynamically pull in configuration at run time from Azure App Configuration  https://docs.microsoft.com/en-us/azure/azure-app-configuration/
                    if (Environment.GetEnvironmentVariable("AzureAppConfig") is null)
                    {
                        throw new Exception("The AzureAppConfig env variable is missing that is required to read variables from Azure App Configuration.  Follow" +
                           "Instructions at https://orlandohealth.visualstudio.com/SDEV/_wiki/wikis/Orlando%20Health%20-%20SDEV%20Wiki/31/Adding-Azure-App-Configuration-to-an-application");
                    }
                    config.AddAzureAppConfiguration(options =>
                        options
                          .Connect(Environment.GetEnvironmentVariable("AzureAppConfig"))
                            //Configure a global refresh variable for this application
                            .ConfigureRefresh(refresh =>
                            {
                                refresh.Register(AzureConfigApplicationName + ":Settings:RefreshSettings", refreshAll: true)
                                      .SetCacheExpiration(new TimeSpan(0, 5, 0));
                            })
                          // Load global settings
                          .Select("Global.*", LabelFilter.Null)
                          // Load configuration values with no label.  AKA prod
                          .Select(AzureConfigApplicationName + ".*", LabelFilter.Null)
                          // Override with any configuration values specific to current hosting env
                          // Stack the changes per https://docs.microsoft.com/en-us/azure/azure-app-configuration/howto-best-practices
                          // How to set env variables https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-3.1#determining-the-environment-at-runtime
                          .Select("Global.*", hostingContext.HostingEnvironment.EnvironmentName)
                          .Select(AzureConfigApplicationName + ".*", hostingContext.HostingEnvironment.EnvironmentName)

                    );
                })
               .UseStartup<Startup>());
    }
}


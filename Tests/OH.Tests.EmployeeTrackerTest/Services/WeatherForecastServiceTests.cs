using Microsoft.VisualStudio.TestTools.UnitTesting;
using OH.Business.EmployeeTrackerTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OH.Business.EmployeeTrackerTest.Helpers;
using OH.Tests.EmployeeTrackerTest;

namespace OH.Business.EmployeeTrackerTest.Services.Tests
{
    [TestClass()]
    public class WeatherForecastServiceTests
    {
        [TestMethod()]
        public async Task GetWeatherForecastAsyncTest()
        {
            var servicesTestingConfiguration = new ServicesTestingConfiguration();

            if (!servicesTestingConfiguration.RunIntegrationTest())
            {
                Assert.IsTrue(true);
            }
            else
            {
                var serviceProvider = servicesTestingConfiguration.CreateConfigurationForTest();
                var weatherForecastService = serviceProvider.GetService<IWeatherForecastService>();
                if (weatherForecastService == null)
                {
                    Assert.Fail("Unable to create service");
                }
                var result = await weatherForecastService.GetWeatherForecastAsync();
                Assert.IsTrue(result.Any());


            }
        }
    }
}
using OH.UI.EmployeeTrackerTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using OH.Business.EmployeeTrackerTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using OH.Tests.EmployeeTrackerTest;

namespace OH.UI.EmployeeTrackerTest.Helpers.Tests
{
    [TestClass()]
    public class WeatherForecastHelperTests
    {
        [TestMethod()]
        public async Task GetWeatherForecastAsyncTest()
        {
            var uiTestingConfiguration = new UITestingConfiguration();

            if (!uiTestingConfiguration.RunIntegrationTest())
            {
                Assert.IsTrue(true);
            }
            else
            {
                var serviceProvider = uiTestingConfiguration.CreateConfigurationForTest();
                var weatherForecastHelper = serviceProvider.GetService<IWeatherForecastHelper>();
                if (weatherForecastHelper == null)
                {
                    Assert.Fail("Unable to create service");
                }
                var (weatherResultList, errorMessage) = await weatherForecastHelper.GetWeatherForecastAsync();
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    Assert.Fail(errorMessage);
                }
                Assert.IsTrue(weatherResultList.Any());


            }
        }
    }
}

namespace OH.Business.EmployeeTrackerTest.Helpers.Tests
{
    [TestClass()]
    public class WeatherForecastHelperTests
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
                var weatherForecastHelper = serviceProvider.GetService<IWeatherForecastHelper>();
                if (weatherForecastHelper == null)
                {
                    Assert.Fail("Unable to create service");
                }
                var result = await weatherForecastHelper.GetWeatherForecastAsync();
                Assert.IsTrue(result.Any());


            }
        }
    }
}
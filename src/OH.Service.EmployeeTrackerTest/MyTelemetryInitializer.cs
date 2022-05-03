using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace OH.Service.EmployeeTrackerTest
{
    public class MyTelemetryInitializer : ITelemetryInitializer
    {

        public void Initialize(ITelemetry telemetry)
        {

            if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
            {
                telemetry.Context.Cloud.RoleName = Program.AzureConfigApplicationName;
                telemetry.Context.Cloud.RoleInstance = Environment.MachineName;

            }
        }
    }
}

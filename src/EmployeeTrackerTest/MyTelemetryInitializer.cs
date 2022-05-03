using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace EmployeeTrackerTest
{
    public class MyTelemetryInitializer : ITelemetryInitializer
    {

        public void Initialize(ITelemetry telemetry)
        {

            if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
            {
                //set custom role name here
                telemetry.Context.Cloud.RoleName = Program.AzureConfigApplicationName;
                telemetry.Context.Cloud.RoleInstance = Environment.MachineName;

            }
        }
    }

}

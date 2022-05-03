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
using Microsoft.ApplicationInsights.Extensibility;

namespace OH.Service.EmployeeTrackerTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMicrosoftIdentityWebApiAuthentication(Configuration, "AzureAd")
            .EnableTokenAcquisitionToCallDownstreamApi()
            .AddInMemoryTokenCaches();

           


            //Application Insights Auto Added by the template
            services.AddSingleton<ITelemetryInitializer, MyTelemetryInitializer>();
            services.AddApplicationInsightsTelemetry(Configuration["Global.Settings.AppInsightsKey"]);

            services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new OpenApiInfo { Title = "OH.Service.EmployeeTrackerTest Api", Version = "v1" });
                  //TODO UNCOMMENT THIS WHEN YOUR APPLICATION IS SETUP IN AZURE
                  //c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                  //{
                  //    Type = SecuritySchemeType.OAuth2,
                  //    Flows = new OpenApiOAuthFlows
                  //    {
                  //        Implicit = new OpenApiOAuthFlow
                  //        {
                  //            AuthorizationUrl = new Uri("https://login.microsoftonline.com/orlandohealth.com/oauth2/v2.0/authorize", UriKind.RelativeOrAbsolute),
                  //            Scopes = new Dictionary<string, string>
                  //              {
                  //                  { Configuration[Program.AzureConfigApplicationName + ".Settings.Scope.UserImpersonation"], "Access API" }
                  //              }
                  //        }
                  //    }
                  //});
                  //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                  //  {
                  //                   {
                  //                       new OpenApiSecurityScheme
                  //                       {
                  //                           Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                  //                       },
                  //                       new[] { "Access OH.Service.EmployeeTrackerTest" }
                  //                   }
                  //  });
              });


            services.AddControllers();
      
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("v1/swagger.json", "OH.Service.EmployeeTrackerTestApi");

                c.OAuthConfigObject = new Swashbuckle.AspNetCore.SwaggerUI.OAuthConfigObject()
                {
                    ClientId = Configuration["Global.Settings.SwaggerClientId"],
                    AppName = "OH.Service.Template"
                };
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

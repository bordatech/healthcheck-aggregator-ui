using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCore.HealthChecks.Aggregator.UI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecksUI(settings =>
            {
                settings.SetHeaderText(Configuration.GetHealthCheckOptions().HeaderText);
                settings.SetEvaluationTimeInSeconds(Configuration.GetHealthCheckOptions().PollingInterval);

                Configuration.GetHealthCheckServices().ForEach(service =>
                {
                    settings.AddHealthCheckEndpoint(service.Name, service.Url);
                });
            }).AddInMemoryStorage();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecksUI(setup =>
                {
                    setup.ApiPath = Configuration.GetHealthCheckOptions().ApiPath;
                    setup.UIPath = Configuration.GetHealthCheckOptions().UIPath;
                });
            });
        }
    }
}
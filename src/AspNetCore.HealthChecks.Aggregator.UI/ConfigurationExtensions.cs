using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace AspNetCore.HealthChecks.Aggregator.UI
{
    public static class ConfigurationExtensions
    {
        public static HealthCheckOptions GetHealthCheckOptions(this IConfiguration configuration)
        {
            HealthCheckOptions options = new HealthCheckOptions();
            configuration.GetSection("HealthCheck").Bind(options);
            return options;
        }
        
        public static List<HealthCheckServiceOptions> GetHealthCheckServices(this IConfiguration configuration)
        {
            List<HealthCheckServiceOptions> healthCheckServices = new List<HealthCheckServiceOptions>();
            configuration.GetSection("Services").Bind(healthCheckServices);

            return healthCheckServices.OrderBy(o => o.Name).ToList();
        }
    }
}
using LoggingApi.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace LoggingApi.Configuration
{
    public static class Neo4jConfiguration
    {
        public static IServiceCollection AddNeo4j(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DbContext>(provider =>
            {
                return new DbContext(configuration);
            });

            return services;
        }
    }
}

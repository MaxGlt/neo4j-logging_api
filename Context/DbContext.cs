using Microsoft.Extensions.Configuration;
using Neo4j.Driver;
using System;
using System.Threading.Tasks;

namespace LoggingApi.Context
{
    public class DbContext : IDisposable
    {
        private static IDriver _driver = null!;

        public DbContext(IConfiguration configuration)
        {
            var neo4jUrl = configuration.GetConnectionString("Neo4j");
            var username = configuration["Neo4j:Username"];
            var password = configuration["Neo4j:Password"];

            _driver = GraphDatabase.Driver(neo4jUrl, AuthTokens.Basic(username, password));
        }

        public async Task LogRequestToNeo4j(string url, string method, TimeSpan duration, string fromApp, string toApp)
        {
            try
            {
                await using var session = _driver.AsyncSession();
                await session.ExecuteWriteAsync(async tx =>
                {
                    await tx.RunAsync(
                        "MERGE (fromApp:Application {name: $fromApp}) " +
                        "MERGE (toApp:Application {name: $toApp}) " +
                        "CREATE (fromApp)-[:CALLS {url: $url, method: $method, duration: $duration}]->(toApp)",
                        new { url, method, duration = duration.TotalMilliseconds, fromApp, toApp });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging request to Neo4j: {ex.Message}");
                throw; // Re-throwing exception to be caught by the calling method
            }
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}

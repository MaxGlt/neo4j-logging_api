using LoggingApi.Context;
using LoggingApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LoggingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggingController : ControllerBase
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<LoggingController> _logger;

        public LoggingController(ILogger<LoggingController> logger, DbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost(Name = "LogRequest")]
        public async Task<IActionResult> Post([FromBody] Neo4jDto neo4jDto)
        {
            if (neo4jDto == null)
            {
                _logger.LogError("Received a null Neo4jDto.");
                return BadRequest("Request body cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Received invalid Neo4jDto: {ModelStateErrors}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Attempting to log request from {FromApp} to {ToApp}", neo4jDto.FromApp, neo4jDto.ToApp);

                await _dbContext.LogRequestToNeo4j(
                    neo4jDto.Url?.ToString() ?? "Unknown URL",
                    neo4jDto.Method ?? "Unknown Method",
                    TimeSpan.FromMilliseconds(neo4jDto.Duration),
                    neo4jDto.FromApp ?? "API",
                    neo4jDto.ToApp ?? "API");

                _logger.LogInformation("Request logged successfully from {FromApp} to {ToApp}", neo4jDto.FromApp, neo4jDto.ToApp);

                return Ok("Request logged successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging the request to Neo4j.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

using LoggingApi.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configuring Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Loading configurations from appsettings.json
var configuration = builder.Configuration;

// Add services to the container
builder.Services.AddControllers();

// Add Neo4j service by passing the configuration
builder.Services.AddNeo4j(configuration);

// Read the configuration for Swagger
bool swaggerEnabled = configuration.GetValue<bool>("Swagger:Enabled");

if (swaggerEnabled)
{
    // Configuring Swagger services for API documentation
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// Redirect to Swagger if Swagger is enabled and the environment is development
if (swaggerEnabled)
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Automatically redirect to Swagger
    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger/index.html");
        }
        else
        {
            await next();
        }
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();

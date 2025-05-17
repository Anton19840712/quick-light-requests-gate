var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services
    .AddEventServices()
    .AddNetworkServices()
    .AddBackgroundServices()
    .AddMessageServingServices()
    .AddRabbitMqServices(builder.Configuration)
    .AddMongoDb(builder.Configuration);

// Configure logging
LoggingConfiguration.ConfigureLogging(builder);
using Hl7Engine.AppHost.Bootstrappers;
using HL7Engine.Module.Tracking.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

var directoryConfiguration = $"{AppContext.BaseDirectory}/Configuration/";

#region Configuration
 var builder = Host.CreateApplicationBuilder(args);
 builder.Configuration.SetBasePath(directoryConfiguration).AddJsonFile("AppHost.json", optional: false);
#endregion 
builder.Services.LoggerConfigExtensions(builder.Configuration);
builder.Services.CoreExtensions(builder.Configuration);
builder.Services.ModulesExtensions(builder.Configuration);


var app = builder.Build();

await app.Services.MigrateTrackingDatabaseAsync();

await app.RunAsync();
 
 
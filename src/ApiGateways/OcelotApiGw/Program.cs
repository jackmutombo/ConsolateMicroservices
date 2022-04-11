using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging((hostingContext, loggingbuilder) =>
{
  builder.Configuration.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
  loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
  loggingbuilder.AddConsole();
  loggingbuilder.AddDebug();
});

builder.Services.AddOcelot()
  .AddCacheManager(settings => settings.WithDictionaryHandle());
var app = builder.Build();

await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();

namespace Catalog.API.Middleware
{
  using Microsoft.AspNetCore.Mvc;
  using System.Text.Json;

  public class ExceptionMiddlerware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddlerware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddlerware(RequestDelegate next, ILogger<ExceptionMiddlerware> logger, IHostEnvironment env)
    {
      _next = next ?? throw new ArgumentNullException(nameof(next));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _env = env ?? throw new ArgumentNullException(nameof(env));
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch(Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;

        var response = new ProblemDetails
        {
          Status = 500,
          Detail = _env.IsDevelopment() ? ex.StackTrace?.ToString() : null,
          Title = ex.Message,
        };

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
      }
    }
  }
}

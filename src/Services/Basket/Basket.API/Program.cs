using Basket.API.GrpcServices;
using Basket.API.Helpers;
using Basket.API.Middleware;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Redis Configuration
builder.Services.AddStackExchangeRedisCache(option =>
{
  option.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
});

// General Configuartion
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(opt =>
  {
    AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
    configuration.Bind("JWTSettings", authenticationConfiguration);
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.TokenKey)),
      ValidIssuer = authenticationConfiguration.ValidIssuer,
      ValidAudience = authenticationConfiguration.ValidAudience,
      ValidateIssuerSigningKey = true,
      ValidateAudience = true,
      ValidateIssuer = true,
      ValidateLifetime = true,
    };
  });

//Grpc Configuartion
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
                          opt.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]));
builder.Services.AddScoped<DiscountGrpcService>();

// MassTrabsit-RabbitMq Configuration
builder.Services.AddMassTransit(config =>
{
  config.UsingRabbitMq((ctx, cfg) =>
  {
    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
  });
});
builder.Services.AddMassTransitHostedService();

builder.Services.AddCors();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddlerware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseCors(opt =>
{
  CORSconfiguration corsConfiguration = new CORSconfiguration();
  configuration.Bind("CORSSetting", corsConfiguration);
  opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000", corsConfiguration.ReactWebURL, corsConfiguration.AccountsURL);
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

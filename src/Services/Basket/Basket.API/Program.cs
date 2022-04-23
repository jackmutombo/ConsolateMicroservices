using Basket.API.GrpcServices;
using Basket.API.Middleware;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Redis Configuration
builder.Services.AddStackExchangeRedisCache(option =>
{
  option.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

// General Configuartion
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddAutoMapper(typeof(Program));

//Grpc Configuartion
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
                          opt.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));
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
  opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000", "http://localhost:3001");
});

app.UseAuthorization();

app.MapControllers();

app.Run();

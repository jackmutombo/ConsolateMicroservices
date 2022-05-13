using Accounts.Data;
using Accounts.Entities;
using Accounts.Extensions;
using Accounts.Helpers;
using Accounts.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
  c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
  {
    Description = "Jwt auth header",
    Name = "Authorization",
    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
  });
  c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        },
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header,
      },
      new List<string>()
    }
  });
});

builder.Services.AddDbContext<StoreContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("AccountConnectionString")));

builder.Services.AddIdentityCore<User>(opt =>
{
  opt.User.RequireUniqueEmail = true;
  opt.Password.RequireDigit = true;
  opt.Password.RequiredLength = 8;
  opt.Password.RequireNonAlphanumeric = true;
  opt.Password.RequireUppercase = true;
  opt.Password.RequireLowercase = true;
  opt.Password.RequiredUniqueChars = 1;
})
  .AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<StoreContext>();

// TODO check this is supposer to be herer
builder.Services.AddAuthentication(option =>
{
  option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(opt =>
  {
    opt.SaveToken = true;
    opt.RequireHttpsMetadata = false;

    AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
    configuration.Bind("JWTSettings", authenticationConfiguration);

    opt.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidAudience = authenticationConfiguration.ValidAudience,
      ValidIssuer = authenticationConfiguration.ValidIssuer,
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
          .GetBytes(authenticationConfiguration.TokenKey))
    };
  });

builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

 app.MigrateDatabase<StoreContext>((context, service) =>
{
  var logger = service.GetService<ILogger<UserContextSeed>>();
  using var userManger = service.GetRequiredService<UserManager<User>>();
  UserContextSeed.SeedAsync(userManger, logger).Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(opt =>
{
  CORSconfiguration corsConfiguration = new CORSconfiguration();
  configuration.Bind("CORSSetting", corsConfiguration);
  opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000", corsConfiguration.ReactWebURL, corsConfiguration.BasketURL);
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

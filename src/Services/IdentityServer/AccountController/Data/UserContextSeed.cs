namespace Accounts.Data
{
  using Accounts.Entities;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.Extensions.Logging;
  using System.Linq;
  using System.Threading.Tasks;

  public class UserContextSeed
  {
    public static async Task SeedAsync(UserManager<User> userManager, ILogger<UserContextSeed> logger)
    {
      if (!userManager.Users.Any())
      {
        var user = new User
        {
          UserName = "bob",
          Email = "bob@test.com",
        };

        await userManager.CreateAsync(user, "Pa$$w0rd");

        await userManager.AddToRoleAsync(user, "Member");

        var admin = new User
        {
          UserName = "admin",
          Email = "admin@test.com",
        };

        var u1 = await userManager.CreateAsync(admin, "Pa$$w0rd");

        var u2 = await userManager.AddToRolesAsync(admin, new[] {"Member","Admin"});

        logger.LogInformation("Seed database user associated with context {DbContextName}", typeof(UserManager<User>).Name);
      }
    }
  }
}
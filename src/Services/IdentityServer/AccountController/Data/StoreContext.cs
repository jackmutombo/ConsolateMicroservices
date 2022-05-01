
using Accounts.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Data
{
  public class StoreContext : IdentityDbContext<User>
  {
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<IdentityRole>()
        .HasData(
        new IdentityRole { Name = "Member", NormalizedName = "MEMBER" },
        new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
        );  
    }
  }
}

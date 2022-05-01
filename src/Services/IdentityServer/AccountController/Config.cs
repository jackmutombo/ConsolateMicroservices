using IdentityServer4.Models;

namespace Accounts
{
  public class Config
  {
    public static IEnumerable<IdentityResource> IdentityResources =>
      new[]
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResource
        {
          Name = "role",
          UserClaims = new List<string> {"role"}
        }
      };
    public static IEnumerable<ApiScope> ApiScopes =>
      new[] { new ApiScope("BasketAPI.read"), new ApiScope("BasketAPI.write") };

    public static IEnumerable<ApiResource> ApiResources =>
      new[]
      {
        new ApiResource("BasketAPI")
        {
          Scopes = new List<string> { "BasketAPI.read", "BasketAPI.write" },
          ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256())},
          UserClaims = new List<string> { "role"}
        }
      };

    public static IEnumerable<Client> Clients =>
      new[]
      {
        //m2m client credentials flow client
        new Client
        {
          ClientId = "m2m.client",
          ClientName = "Client Credentials Client",
          AllowedGrantTypes = GrantTypes.ClientCredentials,
          ClientSecrets = {new Secret("ClientSecret1".Sha256())},
          AllowedScopes = {"BasketAPI.read", "BasketAPI.write"}
        },

        // interativeclient using code flow + pkce
        new Client
        {
          ClientId = "interactive",
          ClientSecrets = {new Secret("ClientSecret1".Sha256()) },
          AllowedGrantTypes = GrantTypes.Code,
          RedirectUris = {"https://localhost:5002/signin-oidc"}, //TODO change this later
          FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
          PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
          AllowOfflineAccess = true,
          AllowedScopes = {"openid", "profile", "BasketAPI.read" },
          RequirePkce = true,
          RequireConsent = true,
          AllowPlainTextPkce = false
        },
      };
  }
}

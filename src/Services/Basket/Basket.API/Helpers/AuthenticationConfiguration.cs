namespace Basket.API.Helpers
{
  public class AuthenticationConfiguration
  {
    public string TokenKey { get; set; }
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
  }
}

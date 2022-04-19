 namespace Basket.API.Entities
{
  public class ShoppingCart
  {
    public string? UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new();

    public ShoppingCart()
    {

    }

    public ShoppingCart(string username)
    {
      UserName = username;
    }

    public long TotalPrice
    {
      get
      {
        long totalprice = 0;
        foreach (var item in Items)
        {
          totalprice += item.Price * item.Quantity;
        }
        return totalprice;
      }
    }
  }
}

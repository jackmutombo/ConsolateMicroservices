namespace Basket.API.Entities
{
  public class ShoppingCartItem
  {
    public int Quantity { get; set; }
    public string? Color { get; set; }
    public long Price { get; set; }

    // navigation properties
    public string? ProductId { get; set; }
    public string? ProductName { get; set; }
  }
}
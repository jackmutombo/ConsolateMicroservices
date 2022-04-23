namespace Basket.API.Entities
{
  public class Product
  {
    public int Quantity { get; set; } = 1;
    public string Color { get; set; }
    public long Price { get; set; }

    // navigation properties
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ImageFile { get; set; }
  }
}

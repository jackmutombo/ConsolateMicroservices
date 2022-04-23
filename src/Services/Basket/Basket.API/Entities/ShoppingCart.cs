namespace Basket.API.Entities
{
  public class ShoppingCart
  {
    public string BuyerId { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new();

    public ShoppingCart()
    {

    }

    public ShoppingCart(string username)
    {
      BuyerId = username;
    }

    public void AddItem(Product product)
    {
      if (Items.All(item => (item.ProductId != product.ProductId || (item.ProductId == product.ProductId && item.Color != product.Color))))
      {
        Items.Add(new ShoppingCartItem 
        {
          ProductId = product.ProductId,
          Quantity = product.Quantity,
          Color = product.Color,
          Price = product.Price,
          ProductName = product.ProductName,
          ImageFile = product.ImageFile,
        });
        return;
      }

      var existingItem = Items.FirstOrDefault(item => item.ProductId == product.ProductId && item.Color == product.Color);
      if (existingItem != null) existingItem.Quantity += product.Quantity;
    }

    public void RemoveItem(Product product)
    {
      var item = Items.FirstOrDefault(item => item.ProductId == product.ProductId && item.Color == product.Color);
      if (item == null) return;
      item.Quantity -= product.Quantity;
      if (item.Quantity <= 0) Items.Remove(item);
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

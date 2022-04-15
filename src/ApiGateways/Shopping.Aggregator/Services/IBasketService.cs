namespace Shopping.Aggregator.Services
{
  using Shopping.Aggregator.Models;

  public interface IBasketService
  {
    Task<BasketModel> GetBasket(string userName);
  }
}

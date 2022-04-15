namespace AspnetRunBasics.Services
{
  using AspnetRunBasics.Models;
  using System.Threading.Tasks;
  public interface IBasketService
  {
    Task<BasketModel> GetBasket(string userName);
    Task<BasketModel> UpdateBasket(BasketModel model);
    Task CheckoutBasket(BasketCheckoutModel model);
  }
}
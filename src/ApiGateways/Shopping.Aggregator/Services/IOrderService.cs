namespace Shopping.Aggregator.Services
{
  using Shopping.Aggregator.Models;

  public interface IOrderService
  {
    Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
  }
}

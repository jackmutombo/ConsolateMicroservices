using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using EventBus.Messages.Events;

namespace Ordering.API.Mapping
{
  public class orderingProfile : Profile
  {
    public orderingProfile()
    {
      CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
    }
  }
}

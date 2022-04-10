namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
  using MediatR;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using System.Threading;
  using Ordering.Application.Contracts.Persistence;
  using AutoMapper;
  using System;

  public class GetOrderListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
  {
    public readonly IOrderRepository _orderRepository;
    public readonly IMapper _mapper;

    public GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
      _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
      var orderList =  await _orderRepository.GetOrdersByUserName(request.UserName);

      return _mapper.Map<List<OrdersVm>>(orderList);
    }
  }
}

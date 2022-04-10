﻿namespace Ordering.API.EventBusConsumer
{
  using AutoMapper;
  using MassTransit;
  using MediatR;
  using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
  using System.Threading.Tasks;
  using EventBus.Messages.Events;

  public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
  {
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILogger<BasketCheckoutConsumer> _logger;

    public BasketCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
      var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
      var result = await _mediator.Send(command);

      _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id : {neworderId}", result);
    }
  }
}

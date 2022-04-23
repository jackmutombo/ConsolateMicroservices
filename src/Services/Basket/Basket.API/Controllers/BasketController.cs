namespace Basket.API.Controllers
{
  using AutoMapper;
  using Basket.API.Entities;
  using Basket.API.GrpcServices;
  using Basket.API.Repositories;
  using EventBus.Messages.Events;
  using MassTransit;
  using Microsoft.AspNetCore.Mvc;
  using System;
  using System.Net;

  [ApiController]
  [Route("api/v1/[controller]")]
  public class BasketController : ControllerBase
  {
    private readonly IBasketRepository _repository;
    private readonly DiscountGrpcService _discountGrpcService;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
      _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    }

    [HttpGet(Name = "GetBasket")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> GetBasket()
    {
      var basket = await RetrieveBasket();
      if (basket == null) return BadRequest(new ProblemDetails {Title ="Product Not Found" });
      return Ok(basket ?? CreateBasket());
    }

    [Route("[action]", Name = "AddItem")]
    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> AddItem([FromBody] Product product)
    {
      //get basket
      var basket = await RetrieveBasket();

      // create basket
      if (basket == null) basket = CreateBasket();

      // TODO check if product exist in the catalog 
      //var productCatalog = getProductCatalog(product);
      //if (productCatalog == null) return NotFound();

      // add item
      basket.AddItem(product);

      // save changes
      await updateBasketDiscountGRP(basket);

     return CreatedAtRoute("GetBasket", basket);

      //return BadRequest(new ProblemDetails { Title = "Problem saving item to basket" });
    }

    
    [Route("[action]", Name = "removeItem")]
    [HttpPost]
    //[ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveItem([FromBody] Product product)
    {
      // get basket
      var basket = await RetrieveBasket();
      if (basket == null) return NotFound();

      // remove item or reduce quantity
      basket.RemoveItem(product);

      // save changes
      await updateBasketDiscountGRP(basket);

      return CreatedAtRoute("GetBasket", basket);

      //return BadRequest(new ProblemDetails { Title = "Problem removing item to basket" });
    }

    [HttpPost(Name ="updateBasket")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
      // TODO : communicate with Discount.Grpc ( done)
      // and calculate  latest prices of product into the shoppinf cart

      var updatedBasket = await updateBasketDiscountGRP(basket);

      // TODO check if the products exists? still need to make sure no bug 

      return Ok(updatedBasket);
    }



    [HttpDelete(Name = "DeleteBasket")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket()
    {
      var basket = await RetrieveBasket();
      if (basket == null) return NotFound();
      await _repository.DeleteBasket(Request.Cookies["buyerId"]);
      return Ok();
    }

    [Route("[action]")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
      // get existing basket with total price 
      // Create basketCheckoutEvent -- Set TotalPrice on basketCheckout eventMessage
      // send checkout event to rabbitmq
      // remove the basket

      // get existing basket with total price
      var basket = await _repository.GetBasket(basketCheckout.UserName);
      if (basket == null)
      {
        return BadRequest();
      }

      // send checkout event to rabbitmq
      var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
      eventMessage.TotalPrice = basket.TotalPrice;
      await _publishEndpoint.Publish(eventMessage);

      // remove the basket
      await _repository.DeleteBasket(basket.BuyerId);

      return Accepted();
    }

    private ShoppingCart CreateBasket()
    {
      var buyerId = Guid.NewGuid().ToString();
      var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(90) };
      Response.Cookies.Append("buyerId", buyerId, cookieOptions);
      var basket = new ShoppingCart { BuyerId = buyerId };
      return basket;
    }

    private async Task<ShoppingCart> RetrieveBasket()
    {
      string buyerId = Request.Cookies["buyerId"];
      if (string.IsNullOrEmpty(buyerId)) return null;
      var basket = await _repository.GetBasket(buyerId);
      return basket;
    }

    private async Task<ShoppingCart> updateBasketDiscountGRP(ShoppingCart basket)
    {
      // consume Discount Grpc // recomendation to use ParallelHelper
      foreach (var item in basket.Items)
      {
        var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
        item.Price -= coupon.Amount;
      }

      return await _repository.UpdateBasket(basket);
    }
  }
}
﻿using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
  public class BasketRepository : IBasketRepository
  {
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache redisCache)
    {
      _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
    }

    public async Task<ShoppingCart> GetBasket(string userName)
    {
      var basket = await _redisCache.GetStringAsync(userName);

      if (String.IsNullOrEmpty(basket))
        return null;

      return JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
      await _redisCache.SetStringAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));

      return await GetBasket(basket.BuyerId);
    }

    public Task DeleteBasket(string userName)
    {
      return _redisCache.RemoveAsync(userName);
    }
  }
}
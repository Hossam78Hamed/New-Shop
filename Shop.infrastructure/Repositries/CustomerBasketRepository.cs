using Shop.Domain.Redis_Entities;
using Shop.Core.interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shop.infrastructure.Repositries
{
    public class CustomerBasketRepository : ICustomerBasketRepository
    {
        private readonly IDatabase database;
        public CustomerBasketRepository(IConnectionMultiplexer connectionMultiplexer) {

            database=connectionMultiplexer.GetDatabase();

        }
        public async Task<CustomerBasket> AddOrUpdateBasketAsync(CustomerBasket customerBasket)
        {
            //TimeSpan.FromDays(3)
            var ok = await database.StringSetAsync(customerBasket.Id,JsonSerializer.Serialize(customerBasket),TimeSpan.FromHours(3));
            if (ok==true) { 
            CustomerBasket? customerBasketem = await GetBasketAsync(customerBasket.Id);
            return customerBasketem;

            }
            else return null;

        }

        public async Task<bool> DeleteBasketAsync(string Id)
        {
            
          return await database.KeyDeleteAsync(Id);


        }
      

        public async Task<CustomerBasket> GetBasketAsync(string Id)
        {
            var Item=await database.StringGetAsync(Id);

            if (!string.IsNullOrEmpty(Item)) {
                CustomerBasket? customerBasket = JsonSerializer.Deserialize<CustomerBasket>(Item);
                return customerBasket;
            }
            return null;

        }
    }
}

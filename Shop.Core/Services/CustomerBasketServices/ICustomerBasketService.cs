using Shop.Domain.Redis_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Services.CustomerBasketServices
{
    public interface ICustomerBasketService
    {
        Task<bool> DeleteBasketAsync(string Id);
        Task<CustomerBasket> GetBasketAsync(string Id);
        Task<CustomerBasket> AddOrUpdateBasketAsync(CustomerBasket customerBasket);

    }
}

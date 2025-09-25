using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Domain.Redis_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.interfaces
{
    public interface  ICustomerBasketRepository
    {
        //Task<bool> DeleteCustomerBasketAsync(string basketId);
        //Task<bool> UpdateCustomerBasketAsync(Entities.Redis_Entities.CustomerBasket customerBasket);
        //Task<Entities.Redis_Entities.CustomerBasket?> GetCustomerBasketAsync(string basketId);

        Task<bool> DeleteBasketAsync(string Id);
        Task<CustomerBasket> GetBasketAsync(string Id);
        Task<CustomerBasket> AddOrUpdateBasketAsync(CustomerBasket customerBasket);

    }
}

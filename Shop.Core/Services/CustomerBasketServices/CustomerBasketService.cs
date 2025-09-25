using Shop.Domain.Redis_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.DTO.Shared;
using Shop.Core.interfaces;
using System.Reflection.Metadata.Ecma335;

namespace Shop.Core.Services.CustomerBasketServices
{
    public class CustomerBasketService : ICustomerBasketService
    {
        private readonly ICustomerBasketRepository customer;
        public CustomerBasketService(ICustomerBasketRepository _customerBasketRepository) {
            customer = _customerBasketRepository;
        }
        public async Task< CustomerBasket> AddOrUpdateBasketAsync(CustomerBasket customerBasket)
        {
            //ResultView< CustomerBasket> resultView = new ResultView<CustomerBasket>();  
            
            CustomerBasket Item = await customer.AddOrUpdateBasketAsync(customerBasket);
            if (Item != null) {
                return Item;

            }
            return null;
        }

        public async Task<bool> DeleteBasketAsync(string Id)
        {
            var ok=await customer.DeleteBasketAsync(Id);
            return ok;
        }

        public async Task<CustomerBasket> GetBasketAsync(string Id)
        {
            CustomerBasket? customerBasket = await customer.GetBasketAsync(Id);

            if (customerBasket != null) { return customerBasket; }
            else return null;
        }
    }
}

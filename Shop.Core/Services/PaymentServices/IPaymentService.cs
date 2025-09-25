using Shop.Domain.Orders;
using Shop.Domain.Redis_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentAsync(string basketId, int? deliveryId);
        //Task<Order> UpdateOrderSuccess(string PaymentInten);
        //Task<Order> UpdateOrderFaild(string PaymentInten);
    }
}

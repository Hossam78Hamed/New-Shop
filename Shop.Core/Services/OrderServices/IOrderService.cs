using Shop.Core.DTO.AlldeliveryMethod;
using Shop.Core.DTO.AllOrderDTO;
using Shop.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Services.OrderServices
{
    public interface IOrderService
    {
        Task<Order> CreateOrdersAsync(OrderDTO orderDTO,string BuyerEmail);
        Task<List<OrderToReturnDTO>> GetAllOrdersForUserAsync(string BuyerEmail);
       Task<OrderToReturnDTO> GetOrderByIdAsync(int Id, string BuyerEmail);
        Task<List<DeliveryMethodDTO>> GetDeliveryMethodAsync();
    }
}

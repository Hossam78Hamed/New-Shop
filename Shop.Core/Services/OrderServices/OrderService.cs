using Shop.Core.DTO.AllOrderDTO;
using Shop.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Services.CustomerBasketServices;
using Shop.Domain.Redis_Entities;
using Shop.Core.Services.ProductServices;
using AutoMapper;
using Shop.Core.interfaces;
using Shop.Core.Services.DeliveryMethodServices;
using Shop.Core.DTO.AlldeliveryMethod;
using Microsoft.Identity.Client;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Stripe;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
namespace Shop.Core.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerBasketService customerBasketService;
        private readonly IProductService productService;
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly IDeliveryMethodService deliveryMethodService;
        public OrderService(ICustomerBasketService _customerBasketService,IProductService _productService,IMapper _mapper ,IDeliveryMethodService _deliveryMethodService, IOrderRepository _orderRepository)
        {

            customerBasketService = _customerBasketService;
            productService = _productService;
            mapper = _mapper;
            deliveryMethodService = _deliveryMethodService;
            orderRepository = _orderRepository;
        }
        public async Task<Order> CreateOrdersAsync(OrderDTO orderDTO,string BuyerEmail)
        {

            //----------------- create orderItems 

            CustomerBasket? basket = await customerBasketService.GetBasketAsync(orderDTO.basketId);
            if (basket == null || !basket.BasketItems.Any())

                throw new Exception("Basket is empty or not found.");

            List<OrderItem>orderItems = new List<OrderItem>();
            foreach (var item in basket.BasketItems)
            {
                var product=(await productService.GetByIdAsync(item.Id)).Data;

                if (product == null)
                    throw new Exception($"Product with ID {item.Id} not found.");
                
                // ME product Removed from DB 
                //
                //var productRemovedDB =await productService.SoftDeleteAsync(item.Id);
        
                OrderItem orderItem = new OrderItem() {
    ProductItemId= item.Id,
      ProductName = product.Name,
      MainImage =item.Image,
       Price=item.Price,
    Quntity=item.Qunatity

          };
            
                orderItems.Add(orderItem);
            }
            //------------------create ShippingAddress
            var shippingAddress = mapper.Map<ShippingAddress>(orderDTO.shipAddress);

            //------------------create DeliveryMethodServices  

            var deliveryDTO=await deliveryMethodService.GetByIdAsync(orderDTO.deliveryMethodId);
           DeliveryMethod?  delivery= mapper.Map<DeliveryMethod>(deliveryDTO);



            //------------------create SubTotal

            decimal price=orderItems.Sum(item => item.Price * item.Quntity);
            //Me
            decimal totalPrice= price + delivery.Price;

            ///not undertstand PaymentIntentId in order
            //var ExisitOrder = await _context.Orders.Where(m => m.PaymentIntentId == basket.PaymentIntentId).FirstOrDefaultAsync();

            //if (ExisitOrder is not null)
            //{
            //    _context.Orders.Remove(ExisitOrder);
            //    await _paymentService.CreateOrUpdatePaymentAsync(basket.PaymentIntentId, deliverMethod.Id);
            //}


            //------------------create order  

            Order orders = new Order();
            orders.shippingAddress= shippingAddress;
           // orders.deliveryMethod = delivery;
           orders.DeliveryMethodId= orderDTO.deliveryMethodId;
            orders.orderItems= orderItems;
            orders.BuyerEmail= BuyerEmail;
            orders.SubTotal = totalPrice; 

            var ordersDS = await orderRepository.AddAsync(orders);
            //var count = await orderRepository.SaveChangesAsync();
            var BasketItemRemoved = await customerBasketService.DeleteBasketAsync(orderDTO.basketId);
            //var orderToReturnDTO = mapper.Map<OrderToReturnDTO>(ordersDS); 

          


                return ordersDS;



            //---------
        }

        public async Task<List<OrderToReturnDTO>> GetAllOrdersForUserAsync(string BuyerEmail)
        {
            var orders = (await orderRepository.GetAllAsync()).Where (o=>o.BuyerEmail==BuyerEmail).ToList();



            if (orders != null)
            {
                var result = mapper.Map<List<OrderToReturnDTO>>(orders);
                result = result.OrderByDescending(m => m.Id).ToList();
             
                //foreach (var item in result)
                //{
                //    var order = orders.FirstOrDefault(o => o.Id == item.Id);
                //    item.Total = order.GetTotal();
                //    //item.status = order.status.ToString();
                //    //item.deliveryMethod = order.deliveryMethod.ShortName;
                //}
               
                return result;

            }
            else return null;



        }

        public async Task<List<DeliveryMethodDTO>> GetDeliveryMethodAsync()
        {
            var deliveryMethods= await deliveryMethodService.GetAllAsync();

            if (deliveryMethods != null) {

                return deliveryMethods;

            } 
            else return null;
                 


        }

        public async Task<OrderToReturnDTO> GetOrderByIdAsync(int Id, string BuyerEmail)
        {
            var order=(await orderRepository.GetAllAsync()).FirstOrDefault(o=>o.Id==Id&&o.BuyerEmail==BuyerEmail);


            
            //
            if (order != null)
            {
                var orderToReturn = mapper.Map<OrderToReturnDTO>(order);
               //I  
               // orderToReturn.Total= order.GetTotal();
                return orderToReturn;

            }
            else return null;
        }












        //---------
    }
}

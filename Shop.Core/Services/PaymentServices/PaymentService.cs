using Microsoft.Extensions.Configuration;
using Shop.Domain.Redis_Entities;
using Shop.Core.Services.CustomerBasketServices;
using Shop.Core.Services.DeliveryMethodServices;
using Shop.Core.Services.ProductServices;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly ICustomerBasketService customerBasketService;
        private readonly IDeliveryMethodService deliveryMethodService;
        private readonly IConfiguration configuration;
        private readonly IProductService productService;

        public PaymentService(ICustomerBasketService _customerBasketService, IDeliveryMethodService _deliveryMethodService,IConfiguration _configuration,IProductService _productService )
        {
            customerBasketService = _customerBasketService;
            deliveryMethodService = _deliveryMethodService;
            configuration = _configuration;
            productService = _productService;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentAsync(string basketId, int? deliveryId)
        {

            CustomerBasket? basket = await customerBasketService.GetBasketAsync(basketId);

            StripeConfiguration.ApiKey = configuration["StripSetting:secretKey"];
            decimal shippingPrice = 0m;

            if (deliveryId.HasValue) { 
          var deliveryMethod = await deliveryMethodService.GetByIdAsync(deliveryId.Value);

                shippingPrice = deliveryMethod.Price;


            }

            foreach (var item in basket.BasketItems)
            {
                var productDTO = (await productService.GetByIdAsync(item.Id)).Data;

                item.Price = productDTO.NewPrice;


            }

            PaymentIntentService paymentIntentService = new PaymentIntentService();
            PaymentIntent _intent = new PaymentIntent(); //PaymentIntent _intent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(m => m.Qunatity * (m.Price * 100)) + (long)(shippingPrice * 100),

                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }

                };
                _intent = await paymentIntentService.CreateAsync(option);
                basket.PaymentIntentId = _intent.Id;
                basket.ClientSecret = _intent.ClientSecret;

            }
            else {
                var option = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(m => m.Qunatity * (m.Price * 100)) + (long)(shippingPrice * 100),


                };
                _intent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId,option);


            }
            CustomerBasket? newBasket = await customerBasketService.AddOrUpdateBasketAsync(basket);
            return newBasket;
            //await work.CustomerBasket.UpdateBasketAsync(basket);
            //return basket;



        }
    }
}

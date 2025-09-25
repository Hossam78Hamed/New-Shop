using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Redis_Entities
{
    public class CustomerBasket
    {
        //CustomerBasketRepository
        public string Id { get; set; }///way this string
        public List<BasketItem> BasketItems { set; get; } = new List<BasketItem>();
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        //PaymentIntentId ClientSecret
        //public CustomerBasket(string _Id) { ///way
        //        Id = _Id;
        //}
        //    public CustomerBasket()//way
        //    {

        //    }
    }
}

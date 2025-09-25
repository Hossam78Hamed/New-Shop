using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Orders
{
    public class Order:BaseEntity<int>
    {
        public Order() { }
        public Order(string _buyerEmail, decimal _subTotal, ShippingAddress _shippingAddress, DeliveryMethod _deliveryMethod, ICollection<OrderItem> _orderItems, string _PaymentIntentId)
        {
            BuyerEmail = _buyerEmail;
            SubTotal = _subTotal;
            shippingAddress = _shippingAddress;
             deliveryMethod = _deliveryMethod;
            orderItems = _orderItems;
           PaymentIntentId = _PaymentIntentId;
        }
       
        public string BuyerEmail { get; set; }
        public decimal SubTotal { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        
        public string PaymentIntentId { get; set; }
        public Status status { get; set; } = Status.Pending;
        ///----------------
        public  ShippingAddress shippingAddress { get; set; }

        public virtual ICollection<OrderItem> orderItems { get; set; } = new HashSet<OrderItem>();

       public int? DeliveryMethodId { get; set; }
        
        [ForeignKey(nameof(DeliveryMethodId))]
        public virtual DeliveryMethod deliveryMethod { get; set; }



        public decimal GetTotal()
        {
            return SubTotal + deliveryMethod.Price;
        }


    }
}

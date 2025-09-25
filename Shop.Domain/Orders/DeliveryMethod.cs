//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Orders
{
    public class DeliveryMethod:BaseEntity<int>
    {
        public DeliveryMethod()
        {

        }
        public DeliveryMethod(string name, decimal price, string deliveryTime, string description)
        {
            Name = name;
            Price = price;
            DeliveryTime = deliveryTime;
            Description = description;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        //--
     
  
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();    
    }
}

using Shop.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.DTO.AlldeliveryMethod
{
    public class DeliveryMethodDTO
    {
       
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
    }
}


using Shop.Core.DTO.AllOrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.DTO.AllOrderDTO
{
    public class OrderDTO
    {
        public int deliveryMethodId { get; set; }

        public string basketId { get; set; }
        public ShipAddressDTO shipAddress { get; set; }
    }
}




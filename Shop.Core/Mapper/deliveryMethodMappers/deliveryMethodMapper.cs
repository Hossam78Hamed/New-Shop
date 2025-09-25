using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Orders;
using Shop.Core.DTO.AlldeliveryMethod;
namespace Shop.Core.Mapper.deliveryMethodMappers
{
    public class deliveryMethodMapper:Profile
    {
        public deliveryMethodMapper() {
            CreateMap<DeliveryMethod, DeliveryMethodDTO>().ReverseMap();
        }
    }
}

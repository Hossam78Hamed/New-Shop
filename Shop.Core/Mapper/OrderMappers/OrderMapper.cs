using AutoMapper;
using Shop.Core.DTO.AllOrderDTO;
using Shop.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Mapper.OrderMappers
{
    public class OrderMapper:Profile

    {

        public OrderMapper() {

            CreateMap<ShippingAddress, ShipAddressDTO>().ReverseMap();
            //OrderToReturnDTO
            CreateMap<Order ,OrderToReturnDTO>().ReverseMap();
            CreateMap<OrderItem,OrderItemDTO>().ReverseMap();

            CreateMap<Order, OrderToReturnDTO>()
                   .ForMember(d => d.deliveryMethod,
                   o => o.
                   MapFrom(s => s.deliveryMethod.Name != null ? s.deliveryMethod.Name : null))
                    .ForMember(dest => dest.Total,
        opt => opt.MapFrom(src => src.GetTotal())) // <-- Fix here
                        .ReverseMap();



            ////
            //        CreateMap<Order, OrderToReturnDTO>()
            //.ForMember(d => d.deliveryMethod,
            //           o => o.MapFrom(s => s.deliveryMethod.Name))
            //.ReverseMap()
            //.ForMember(o => o.deliveryMethod,
            //           opt => opt.MapFrom(dto => new DeliveryMethod { Name = dto.deliveryMethod }));



        }
    }
}

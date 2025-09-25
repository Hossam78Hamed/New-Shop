using AutoMapper;
using Shop.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.DTO.AllAddressDTO;
namespace Shop.Core.Mapper.AddressMappers
{
    
    public class AddressMapper:Profile
    {
        public AddressMapper()
        {

            CreateMap<AddressDTO, Address>().ReverseMap();
            CreateMap<UpdateAddressDTO, Address>().ReverseMap();
        }
    }

}

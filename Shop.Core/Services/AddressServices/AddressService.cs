using AutoMapper;
using Shop.Core.DTO.AllAddressDTO;
using Shop.Core.DTO.CategoryDTO;
using Shop.Domain.Accounts;
using Shop.Core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Services.AddressServices
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;

        public AddressService(IAddressRepository _addressRepository, IMapper _mapper)
        {
            addressRepository = _addressRepository;
            mapper = _mapper;
        }
        public async Task<List<AddressDTO>> GetAllAsync()
        {
            var listaddress = (await addressRepository.GetAllAsync()).ToList();

            var listaddressDTO = mapper.Map<List<AddressDTO>>(listaddress);

            if (listaddressDTO != null)
            {
                return listaddressDTO;

            }
            else return null;
        }

        public async Task<AddressDTO> GetByIdAsync(int Id)
        {
            var address = (await addressRepository.GetByIdAsync(Id));

            var addressDTO = mapper.Map<AddressDTO>(address);

            if (addressDTO != null)
            {
                return addressDTO;

            }
            else return null;
        }

        public async Task<AddressDTO> AddAsync(AddressDTO entity)
        {
            if (entity == null) { return null; }
            Address address = mapper.Map<Address>(entity);
            Address DBaddress = (await addressRepository.AddAsync(address));
            //await categoryRepository.SaveChangesAsync();
            AddressDTO addressDTO = mapper.Map<AddressDTO>(DBaddress);
            return addressDTO;
        }
        public async Task<AddressDTO> UpdateAsync(AddressDTO entity)
        {

            Address address = mapper.Map<Address>(entity);
            Address DBaddress = (await addressRepository.UpdateAsync(address));
            //  await categoryRepository.SaveChangesAsync();
            AddressDTO addressDTO = mapper.Map<AddressDTO>(DBaddress);
            return addressDTO;
        }
    }
}
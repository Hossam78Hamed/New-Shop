using AutoMapper;
using Shop.Core.DTO.AlldeliveryMethod;
using Shop.Core.DTO.CategoryDTO;
using Shop.Domain.Orders;
using Shop.Domain.Product;
using Shop.Core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Services.DeliveryMethodServices
{
    public class DeliveryMethodService : IDeliveryMethodService
    {
        private readonly IMapper mapper;
        private readonly IDeliveryMethodRepository deliveryMethodRepository;
        public DeliveryMethodService(IDeliveryMethodRepository _deliveryMethodRepository,IMapper _mapper) {
            deliveryMethodRepository = _deliveryMethodRepository;
            mapper = _mapper;
        }

        public async Task<List<DeliveryMethodDTO>> GetAllAsync()
        {
            var deliveryMethodDTO = (await deliveryMethodRepository.GetAllAsync()).ToList() ;

            var deliveryMethod = mapper.Map<List<DeliveryMethodDTO>>(deliveryMethodDTO);

            if (deliveryMethod != null) return deliveryMethod;
            else return null;


        }

        public async Task<DeliveryMethodDTO> GetByIdAsync(int Id)
        {

            var delivery = await deliveryMethodRepository.GetByIdAsync(Id); ;
           
            
            if (delivery != null)
            {
                if (delivery.IsDeleted == false)
                {

                    var deliveryMethodDTO = mapper.Map<DeliveryMethodDTO>(delivery);

                    return deliveryMethodDTO;
                }
            }

            return null;
        }
    }
}

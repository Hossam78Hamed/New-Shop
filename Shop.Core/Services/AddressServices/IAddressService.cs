using Shop.Core.DTO.AllAddressDTO;
using Shop.Core.DTO.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Services.AddressServices
{
    public interface IAddressService
    {
         Task<List<AddressDTO>> GetAllAsync();
         Task<AddressDTO> GetByIdAsync(int Id);
         Task<AddressDTO> AddAsync(AddressDTO entity);
         Task<AddressDTO> UpdateAsync(AddressDTO entity);
        //public Task<GetOneCategoryDTO> HeardDeleteAsync(int Id);
        //public Task<GetOneCategoryDTO> SoftDeleteAsync(int Id);
        //public Task<int> SaveChangesAsync();
    }
}

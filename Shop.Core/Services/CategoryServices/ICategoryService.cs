using Shop.Core.DTO.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Services.CategoryServices
{
    public interface  ICategoryService
    {
        public Task<List<GetOneCategoryDTO>> GetAllAsync();
        public Task<GetOneCategoryDTO> GetByIdAsync(int Id);
        public Task<AddCategoryDTO> AddAsync(AddCategoryDTO entity);
        public Task<UpdateCategoryDTO> UpdateAsync(UpdateCategoryDTO entity);
        public Task<GetOneCategoryDTO> HeardDeleteAsync(int Id);
        public Task<GetOneCategoryDTO> SoftDeleteAsync(int Id);
        public Task<int> SaveChangesAsync();
    }
}

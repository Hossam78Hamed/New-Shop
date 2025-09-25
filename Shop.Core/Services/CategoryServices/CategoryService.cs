using AutoMapper;
using Shop.Core.DTO.CategoryDTO;
using Shop.Core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Product;
using Shop.Domain.Product;

namespace Shop.Core.Services.CategoryServices
{
    
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository _categoryRepository, IMapper _mapper)
        {

            categoryRepository = _categoryRepository;
            mapper = _mapper;
        }
        public async Task<List<GetOneCategoryDTO>> GetAllAsync()
        {

            var ListCategory = (await categoryRepository.GetAllAsync()).Where (c=>c.IsDeleted==false).ToList();


            var ListCategoryDTO = mapper.Map<List<GetOneCategoryDTO>>(ListCategory);
           return ListCategoryDTO;
        }
        public async Task<AddCategoryDTO> AddAsync(AddCategoryDTO entity)
        {
            Category category = mapper.Map<Category>(entity);
            Category DBcategory = (await categoryRepository.AddAsync(category));
          //await categoryRepository.SaveChangesAsync();
            AddCategoryDTO categoryDTO = mapper.Map<AddCategoryDTO>(DBcategory);
            return categoryDTO;
        }
        public async Task<UpdateCategoryDTO> UpdateAsync(UpdateCategoryDTO entity)
        {

            Category category = mapper.Map<Category>(entity);
            Category DBcategory = (await categoryRepository.UpdateAsync(category));
           //  await categoryRepository.SaveChangesAsync();
            UpdateCategoryDTO categoryDTO = mapper.Map<UpdateCategoryDTO>(DBcategory);
            return categoryDTO;


        }
        public async Task<GetOneCategoryDTO> HeardDeleteAsync(int Id)
        {
            Category category = (await categoryRepository.DeleteAsync(Id));
            GetOneCategoryDTO categoryDTO= mapper.Map<GetOneCategoryDTO>(category);
            await categoryRepository.SaveChangesAsync();
            return categoryDTO;
        }
        public async Task<GetOneCategoryDTO> SoftDeleteAsync(int Id)
        {
            Category category = await categoryRepository.GetByIdAsync(Id);
            category.IsDeleted = true;
            await categoryRepository.SaveChangesAsync();
            GetOneCategoryDTO categoryDTO = mapper.Map<GetOneCategoryDTO>(category);

            return categoryDTO;
        }


        public async Task<GetOneCategoryDTO> GetByIdAsync(int Id)
        {
            Category category = await categoryRepository.GetByIdAsync(Id);
            if (category != null) {
                if (category.IsDeleted == false)
                {

                    GetOneCategoryDTO categoryDTO = mapper.Map<GetOneCategoryDTO>(category);

                    return categoryDTO;
                }
            }
            
             return null;
           
        }

        public async Task<int> SaveChangesAsync()
        {
            int count=await categoryRepository.SaveChangesAsync();
            return count;
        }

        
    }
}

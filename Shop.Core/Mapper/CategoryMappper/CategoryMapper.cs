using AutoMapper;
using Shop.Core.DTO.CategoryDTO;
using Shop.Domain.Product;
namespace Shop.Core.Mapper.CategoryMapper
{
    public class CategoryMapper:Profile
    {
       
        public CategoryMapper() {
            CreateMap<AddCategoryDTO, Category>().ReverseMap();

            CreateMap<UpdateCategoryDTO,Category>().ReverseMap();
            CreateMap<GetOneCategoryDTO, Category>().ReverseMap();
            CreateMap<GetOneCategoryDTO, Category>().ReverseMap();
        }
    }
}

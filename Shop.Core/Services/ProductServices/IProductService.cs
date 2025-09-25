using Shop.Core.DTO.AllProductDTO;
using Shop.Core.DTO.CategoryDTO;
using Shop.Core.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Services.ProductServices
{
    public interface IProductService
    {
        public Task<ResultView<List<ProductDTO>>> GetAllAsync();
        public Task<ResultView<Pagination<List<ProductDTO>>>> GetAllByFilteringAsync(ProductFilter productFilter);

        public Task<ResultView<ProductDTO>> GetByIdAsync(int Id);
        public Task<ResultView< AddProductDTO>> AddAsync(AddProductDTO entity);
        public Task<ResultView<UpdateProductDTO>> UpdateAsync(UpdateProductDTO entity);
        public Task<ResultView<ProductDTO>> HeardDeleteAsync(int Id);
       public Task<ResultView<ProductDTO>> SoftDeleteAsync(int Id);
        public Task<int> SaveChangesAsync();
       
       
    }
}

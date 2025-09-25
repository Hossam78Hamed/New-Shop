using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Core.DTO.AllProductDTO;
using Shop.Core.Entities.Product;
using Shop.Core.interfaces;
using Shop.Core.Mapper.ProductMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Services.PhotoServices;

namespace Shop.Core.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IPhotoService photoService;
        private readonly IMapper mapper;

        public ProductService(IProductRepository _productRepository, IMapper _mapper, IPhotoService _photoService)
        {
            productRepository = _productRepository;
            photoService = _photoService;
            mapper = _mapper;
        }

        public async Task<AddProductDTO> AddAsync(AddProductDTO entity)
        {
            if (entity == null) return new AddProductDTO(); // Problem 2: Return a non-null default object

            Product product = mapper.Map<Product>(entity);
            Product productInDB = await productRepository.AddAsync(product);

            // Problem 3: Check for null before passing to AddImageAsync
            List<string> ImagePaths = entity.Photos != null
                ? await photoService.AddImageAsync(entity.Photos, product.Name)
                : new List<string>();

            List<Photo>? photos = ImagePaths.Select(path => new Photo()
            {
                ProductId = product.Id,
                ImageName = path,
            }).ToList();

            List<Photo>? photosDB = await photoService.AddRangeImageAsync(photos);

            // Map productInDB to AddProductDTO
            AddProductDTO addProductDTO = mapper.Map<AddProductDTO>(productInDB);

            return addProductDTO;
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            List<Product> Products = (await productRepository.GetAllAsync()).ToList();
            List<ProductDTO> ProductDTOs = mapper.Map<List<ProductDTO>>(Products);
            return ProductDTOs;
        }

        public async Task<ProductDTO> GetByIdAsync(int Id)
        {
            Product product = await productRepository.GetByIdAsync(Id);
            ProductDTO productDTO = mapper.Map<ProductDTO>(product);
            return productDTO;
        }

        public Task<int> SaveChangesAsync()
        {
            return productRepository.SaveChangesAsync();
        }

        // Problem 1: Implement missing interface member
        public async Task<res> UpdateAsync(AddProductDTO entity)
        {
            if (entity == null) return new AddProductDTO();

            Product product = mapper.Map<Product>(entity);
            Product updatedProduct = await productRepository.UpdateAsync(product);

            AddProductDTO updatedProductDTO = mapper.Map<AddProductDTO>(updatedProduct);
            return updatedProductDTO;
        }
    }
}

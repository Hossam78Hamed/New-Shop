using AutoMapper;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using Shop.Core.DTO.AllProductDTO;
using Shop.Domain.Product;
using Shop.Core.interfaces;
using Shop.Core.Mapper.ProductMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Services.PhotoServices;
using Shop.Core.DTO.Shared;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
namespace Shop.Core.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IPhotoService photoService;
        private readonly IMapper mapper;
        
        int x3 = 333;
        int x4 = 44;
        public ProductService( IProductRepository _productRepository,IMapper _mapper,IPhotoService _photoService) {
            productRepository = _productRepository;
            photoService = _photoService;
            mapper = _mapper;
        }

        public async Task<ResultView<AddProductDTO>> AddAsync(AddProductDTO entity)
        {
            ResultView<AddProductDTO> resultView = new ResultView<AddProductDTO>(); 
            if (entity == null)
            {
                resultView.IsSuccess = false;
                resultView.Msg = "can not add";
                resultView.Data = null;
                return resultView;
            }
             Product product = mapper.Map<Product>(entity);
            ////
            



                ///
            Product productInDB=await productRepository.AddAsync(product);

            ///add photo
            List<string> ImagePaths = await photoService.AddImageAsync(entity.Photos, product.Name);
            List<Photo>? photos = ImagePaths.Select(path=>new Photo() { 
            ProductId=product.Id,   
            ImageName=path,    
            }).ToList();

            List<Photo>? photosDB=  await photoService.AddRangeImageAsync(photos);
            ///______________here
            //AddProductDTO addProductDTO = mapper.Map<AddProductDTO>(productInDB);

            //if (photosDB == null) return null;
            resultView.IsSuccess = true;
            resultView.Msg = "create Product";
            resultView.Data= new AddProductDTO
            {
                Name = productInDB.Name,
                Description = productInDB.Description,
                NewPrice = productInDB.NewPrice,
                OldPrice = productInDB.OldPrice
                ,
                CategoryName = productInDB.Category.Name
            };
            return resultView;


                }
        public async Task<ResultView<UpdateProductDTO>> UpdateAsync(UpdateProductDTO entity) {


            ResultView<UpdateProductDTO> resulteView=new ResultView<UpdateProductDTO>();
            if (entity != null) {
                var product = (await productRepository.GetByIdAsync(entity.Id));

                if (product != null)
                {
            List<Photo>? photosDelete =( await photoService.GetAllAsync()).Where(x=>x.ProductId==entity.Id).ToList();
            foreach (var item in photosDelete)
            {
                await photoService.HeardDeleteAsync(item.Id);
            }
                    bool ok = await photoService.DeleteImageAsync(product.Name);




                    if (ok == true)
            {
                List<string>? ImagePaths = await photoService.AddImageAsync(entity.Photos, entity.Name);
                var photos = ImagePaths.Select(x => new Photo
                {
                    ImageName = x,
                    ProductId = entity.Id


                }).ToList();
                List<Photo>? photosDB = await photoService.AddRangeImageAsync(photos);

                mapper.Map(entity, product);
                Product? productInDB = await productRepository.UpdateAsync(product);
               // UpdateProductDTO? updateProductDTO = mapper.Map<UpdateProductDTO>(productInDB);
                resulteView.IsSuccess = true;
                resulteView.Msg = "update is success";
                resulteView.Data = new UpdateProductDTO {
                Name=productInDB.Name,Description= productInDB.Description
                ,NewPrice= productInDB.NewPrice,OldPrice=productInDB.OldPrice
                ,CategoryName = productInDB.Category.Name
                };
                return resulteView;
                }


            }
            
            
            ///phote
           
            }
            

                resulteView.IsSuccess = false;
                resulteView.Msg = "can not update";
                resulteView.Data = null;
                return resulteView;
        }
        public async Task<ResultView<ProductDTO>> SoftDeleteAsync(int Id) {
            ResultView<ProductDTO> resultView = new ResultView<ProductDTO>();
            Product? product=await productRepository.GetByIdAsync(Id);
            if (product != null) {
                //photo


                product.IsDeleted = true;

                Product? productInDB = await productRepository.UpdateAsync(product);
                ProductDTO productDTO = mapper.Map<ProductDTO>(product);

                resultView.IsSuccess = true;
                resultView.Msg = "Soft delete is success";
                resultView.Data = productDTO;
                return resultView;
            }

            else
            {

                resultView.IsSuccess = false;
                resultView.Msg = "can not delete";
                resultView.Data = null;
                return resultView;
            }

        }



        public async Task<ResultView<ProductDTO>> HeardDeleteAsync(int Id) {


            ResultView<ProductDTO> resultView = new ResultView<ProductDTO>();
            Product? product = await productRepository.GetByIdAsync(Id);
            if (product != null)
            {

                var listphoto=await photoService.GetAllAsync();
                var listphotoHaveProductId = listphoto.Where(p=>p.ProductId==Id).ToList();
                // var asfa=await photoService.Delete(listphotoHaveProductId);
                foreach (var item in listphotoHaveProductId)
                {
                    var asfa=await photoService.HeardDeleteAsync(item.Id);

                }

                Product? deleteProduct = await productRepository.DeleteAsync(Id);
                //photo
                
                var ok=await photoService.DeleteImageAsync(product.Name);

                List<Photo>? photosDelete = (await photoService.GetAllAsync()).Where(x => x.ProductId == Id).ToList();
                foreach (var item in photosDelete)
                {
                    await photoService.HeardDeleteAsync(item.Id);
                }

                if (ok &&deleteProduct!=null)
                {

                    ProductDTO? productDTO= mapper.Map<ProductDTO>(deleteProduct);
                    resultView.IsSuccess = true;
                    resultView.Msg = "Haerd delete is success";
                    resultView.Data = productDTO;
                    return resultView;

                }
                           
            }

           

                resultView.IsSuccess = false;
                resultView.Msg = "can not delete";
                resultView.Data = null;
                return resultView;
            

            


        }

        public async Task<ResultView<List<ProductDTO>>> GetAllAsync()
        {
            //List<Product> Products = (await productRepository.GetAllAsync()).Include(x=>x.Category).Include(x=>x.Photos).ToList();
           var Products = (await productRepository.GetAllAsync()).Where(x=>x.IsDeleted==false).ToList();

            List<ProductDTO> ProductDTOs = mapper.Map<List<ProductDTO>>(Products);
            ResultView<List<ProductDTO>> resultView = new ResultView<List<ProductDTO>>()
            {IsSuccess=true,Msg="Done",Data=ProductDTOs

            };
            
            return resultView;
            
        }

        public async Task<ResultView<ProductDTO>> GetByIdAsync(int Id)
        {
            ResultView<ProductDTO> resultView=new ResultView<ProductDTO>();
            Product product =( await productRepository.GetByIdAsync(Id));

            if (product != null && product.IsDeleted == false  ) {

                resultView.IsSuccess = true;
                resultView.Msg = "Done";
                resultView.Data = new ProductDTO
                {
                    Name = product.Name,
                    Description = product.Description,
                    NewPrice = product.NewPrice,
                    OldPrice = product.OldPrice
                    ,
                    CategoryName = product.Category.Name
                };
                return resultView;
            }
            //ProductDTO productDTO = mapper.Map<ProductDTO>(product); 

            resultView.IsSuccess = false;
            resultView.Msg = "Not found";
            resultView.Data = null;
            return resultView;


            return resultView;

            
        }

        public Task<int> SaveChangesAsync()
        {
            return productRepository.SaveChangesAsync();
           
        }

        public async Task<ResultView<Pagination<List<ProductDTO>>>> GetAllByFilteringAsync(ProductFilter productFilter)
        {
            ResultView<Pagination<List<ProductDTO>>> resultView = new ResultView<Pagination<List<ProductDTO>>>();
            
            var products = (await productRepository.GetAllAsync());
            if (products != null)
            {


                //serch by name or descrption
                if (productFilter.Search != null)
                {
                    var searchWords = productFilter.Search.ToLower().Split(' ');

                    products = (await productRepository.GetAllAsync()).Where(p => searchWords.
                   All(w => p.Name.ToLower().Contains(w)
                   || p.Description.ToLower().Contains(w)));



                }



                //sortPrice
                if (!string.IsNullOrEmpty(productFilter.sortPriceUpDown)) {
                    if (productFilter.sortPriceUpDown == "priceAsn")
                    {
                        products = products.OrderBy(products => products.NewPrice);

                    }
                    else if (productFilter.sortPriceUpDown == "priceDes")
                    {
                        products = products.OrderByDescending(products => products.NewPrice);
                    }
                    else
                        products = products.OrderBy(products => products.Name);

                }

                //categoryId
                if (productFilter.categoryId.HasValue)
                    products = products.Where(c => c.CategoryId == productFilter.categoryId);
                ///Page
                if (productFilter.PageSize > productFilter.MaxPageSize)
                    productFilter.PageSize=productFilter.MaxPageSize;
                                    products = products.Skip((productFilter.PageNumber - 1) * (productFilter.PageSize)).Take(productFilter.PageSize);
                var productsFiltering = products.ToList();

                var producstDTO = mapper.Map<List<ProductDTO>>(productsFiltering);
                //pagination


                //resultView
                resultView.Data = new Pagination<List<ProductDTO>>(
                    productFilter.PageNumber, productFilter.PageSize,
          await productRepository.CountAsync(), producstDTO

                    );
               
                
              
                resultView.IsSuccess = true;
                resultView.Msg = "Done";
                return resultView;
            }
            resultView.Data = null;
            resultView.IsSuccess = false;
            resultView.Msg = "Wrong";
            return resultView;



        }

       
    }
}

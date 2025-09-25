using AutoMapper;
using Microsoft.Data.SqlClient;
using Shop.Core.DTO.AllProductDTO;
using Shop.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
//
using Shop.Core.DTO.AllProductDTO;
using AutoMapper;
using Shop.Core.DTO.CategoryDTO;
using Shop.Domain.Product;
//
namespace Shop.Core.Mapper.ProductMappers
{
    public class ProductMapper:Profile
    {
      
        public ProductMapper() {

            CreateMap<Product, ProductDTO>().
                ForMember(dest=>dest.CategoryName
                , op=>op.MapFrom
                (src=>src.Category.Name)).ReverseMap();
            ///-----
            ///
            CreateMap<AddProductDTO, Product>()
             .ForPath(dest=>dest.Category.Name,op=>op.MapFrom(src=>src.CategoryName))
           .ForMember(m => m.Photos, op => op.Ignore())
           .ReverseMap();

            CreateMap<UpdateProductDTO , Product>()
           .ForPath(dest => dest.Category.Name, op => op.MapFrom(src => src.CategoryName))
         .ForMember(m => m.Photos, op => op.Ignore())
         .ReverseMap();

            ///--------AddProductDTO
            // من Product -> AddProductDTO
            //CreateMap<Product, AddProductDTO>()
            //    .ForMember(dest => dest.CategoryName,
            //        op => op.MapFrom(src => src.Category.Name))
            //    .ForMember(dest => dest.Photos,
            //        op => op.Ignore()); // لو Update DTO فيه صور

            //// من AddProductDTO -> Product
            //CreateMap<AddProductDTO, Product>()
            //    .ForMember(dest => dest.Photos,
            //        op => op.Ignore()) // الرفع Manual
            //    .ForMember(dest => dest.Category,
            //        op => op.MapFrom(src => new Category { Name = src.CategoryName }));
     //       CreateMap<Product, AddProductDTO>()
     //       .ForMember(dest => dest.Photos,
     //               op => op.Ignore())
     //           .ForMember(dest => dest.CategoryName,
     //    op => op.MapFrom(src => src.Category.Name))
     //.ReverseMap()
     //.ForPath(dest => dest.Category.Name,
     //op => op.MapFrom(src => src.CategoryName));
            
            ////---------UpdateProductDTO----------
            // من Product -> UpdateProductDTO
            //CreateMap<Product, UpdateProductDTO>()
            //    .ForMember(dest => dest.CategoryName,
            //        op => op.MapFrom(src => src.Category.Name))
            //    .ForMember(dest => dest.Photos,
            //        op => op.Ignore()); // لو Update DTO فيه صور

            // من UpdateProductDTO -> Product
            //CreateMap<UpdateProductDTO, Product>()
            //    .ForMember(dest => dest.Photos,
            //        op => op.Ignore()) // الرفع Manual
            //    .ForMember(dest => dest.Category,
            //        op => op.MapFrom(src => new Category { Name = src.CategoryName }));


        }



    }
}

using AutoMapper;
using Microsoft.Identity.Client;
using Shop.Core.DTO.AllPhotoDTO;
using Shop.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Mapper.PhotoMappers
{
    public class PhotoMapper:Profile
    {
       public PhotoMapper() {

            CreateMap<Photo, PhotoDTO>().ReverseMap();
        } 


    }
}

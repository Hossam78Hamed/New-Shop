using Microsoft.AspNetCore.Http;
using Shop.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shop.Core.Services.PhotoServices
{
    public interface IPhotoService
    {
        public Task<List<Photo>> GetAllAsync();
        public Task<Photo> HeardDeleteAsync(int Id);
        public Task<List<string>> AddImageAsync(IFormFileCollection files, string src);

        public  Task<bool> DeleteImageAsync(string src);
        public Task<List<Photo>> AddRangeImageAsync(List<Photo>Photos);    
    }
}

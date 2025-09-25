using Shop.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.interfaces
{
    public interface IPhotoRepository:IGenericRepository<Photo>
    {
          Task<List<Photo>> AddRangeImageInDBAsync(List<Photo> Photos);

    }
}

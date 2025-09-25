using Shop.Domain.Product;
using Shop.Core.interfaces;
using Shop.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.infrastructure.Repositries
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    { ///add future method
        public AppDbContext appDbContext;
        public PhotoRepository(AppDbContext context) : base(context)
        {
            appDbContext= context;
        }
        public async Task<List<Photo>> AddRangeImageInDBAsync(List<Photo> Photos)
        {
             await appDbContext.Photos.AddRangeAsync(Photos);
          await  appDbContext.SaveChangesAsync();
            return Photos;
        }

    }
}

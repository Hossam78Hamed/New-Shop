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
    public class CategoryRepository: GenericRepository<Category>,ICategoryRepository
    { ///add future method
        public CategoryRepository(AppDbContext context):base(context) { }
    }
}

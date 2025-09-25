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
    public class ProductRepository:GenericRepository<Product>,IProductRepository
    {
        ///add future method
        public ProductRepository(AppDbContext context):base(context) { }

        
    }
}

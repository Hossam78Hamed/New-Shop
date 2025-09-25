using Shop.Domain.Orders;
using Shop.Core.interfaces;
using Shop.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.infrastructure.Repositries
{
    public class DeliveryMethodRepository : GenericRepository<DeliveryMethod>, IDeliveryMethodRepository
    {
        public DeliveryMethodRepository(AppDbContext _context) : base(_context)
        {
        }
    }
}

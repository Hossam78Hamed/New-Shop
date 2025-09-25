using Shop.Core.interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Orders;
using Shop.infrastructure.Data;
namespace Shop.infrastructure.Repositries
{
    public class OrderRepository : GenericRepository<Shop.Domain.Orders.Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext _context) : base(_context)
        {
        }
    }
}

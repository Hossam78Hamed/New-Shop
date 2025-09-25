using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Orders;
namespace Shop.Core.interfaces
{
    public interface IOrderRepository:IGenericRepository<Shop.Domain.Orders.Order>
    {
    }
}

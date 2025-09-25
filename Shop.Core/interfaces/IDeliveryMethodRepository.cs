using Shop.Core.Services.DeliveryMethodServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Orders;
namespace Shop.Core.interfaces
{
    public interface IDeliveryMethodRepository:IGenericRepository<DeliveryMethod>
    {

    }
}

using Shop.Domain.Accounts;
using Shop.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.interfaces;
namespace Shop.infrastructure.Repositries
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(AppDbContext _context) : base(_context)
        {
        }
    }
}

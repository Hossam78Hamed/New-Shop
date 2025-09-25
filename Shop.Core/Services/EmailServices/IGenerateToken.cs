using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Accounts;
namespace Shop.Core.Services.EmailServices
{
    public interface IGenerateToken
    {
        public string GetAndCreateToken(ApplicationUser applicationUser);
           
        }
    }



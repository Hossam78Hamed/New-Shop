using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;


namespace Shop.Domain.Accounts
{
    public class ApplicationUser:IdentityUser
    {
       
        public string?  DisplayName { get; set; }

        //public int? AddressID { get; set; }//I am
        //[ForeignKey(nameof(AddressID))]//I am
        public virtual Address Address { get; set; }//wrong way do not  using   virtual
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Accounts
{
public class Address:BaseEntity<int>
    {
       
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? State { get; set; }
        //[ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public virtual ApplicationUser ApplicationUser { set; get; }
    }
}

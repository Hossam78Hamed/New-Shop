using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain
{
    public  class BaseEntity<T>
    {
     
        public T Id { set; get; }
     
        public DateTime? Created { get; set; }
       // public int? CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
       // public int? UpdatedBy { get; set; }
        public DateTime? Deleted { get; set; }
       // public int? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; } = false;

    }
}

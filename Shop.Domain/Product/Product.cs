using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Product
{
    public class Product :BaseEntity<int>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
       ////> [ForeignKey (nameof(CategoryId))]
       public virtual Category? Category { get; set; }
        public virtual ICollection<Photo>?Photos { get; set; }=new HashSet<Photo>();
        
        ////> public virtual List<Photo> Photos1 { get; set; }
    }
}

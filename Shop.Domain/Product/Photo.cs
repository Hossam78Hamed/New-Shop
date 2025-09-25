using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shop.Domain.Product
{
    public  class Photo:BaseEntity<int>
    {
        public string? ImageName { get; set; }
        [ForeignKey("Product")]
        public int ? ProductId { get; set; }
        [JsonIgnore]
        public virtual Product? Product { get; set; }    

    }
}

using Shop.Domain.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.DTO.AllProductDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }

        public string?  CategoryName { get; set; }
        public List<Photo>? Photos { get; set; } =new List<Photo>();
    }
}

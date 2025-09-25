using Microsoft.AspNetCore.Http;
using Shop.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Shop.Core.DTO.AllProductDTO
{
    public class AddProductDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? NewPrice { get; set; }

        public string? CategoryName { get; set; }
        public IFormFileCollection Photos { get; set; }
    }
}

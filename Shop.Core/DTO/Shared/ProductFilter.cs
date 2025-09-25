using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.DTO.Shared
{
    public class ProductFilter
    {
        public string? Search { get; set; }  
        public string? sortPriceUpDown { get; set; } = "priceAsn";
        public int? categoryId { get; set; }// = 11;
        public int MaxPageSize { get; set; } = 6;
        public int PageSize { get; set; } =2;
        public int PageNumber { get; set; } = 1;

    }
}
/*
 * private int _pageSize;
public int PageSize
{
    get => _pageSize;
    set => _pageSize = (value > 100) ? 100 : value; // ✅ cap value at 100
}
 */
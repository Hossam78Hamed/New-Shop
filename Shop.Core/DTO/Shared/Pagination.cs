using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.DTO.Shared
{
    public  class Pagination<T>where T :class
    {
       
        public Pagination(int _pageNumber,int _pageSize,int _totalCount, T _Data) {
            pageNumber= _pageNumber;
            pageSize= _pageSize;    
            totalCount = _totalCount;
            data = _Data;
        
        }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public T? data { get; set; }


    }
}
/*
 *  PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
 */

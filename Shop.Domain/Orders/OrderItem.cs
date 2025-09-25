using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Orders
{
    public class OrderItem: BaseEntity<int>
    {
        public OrderItem()
        {

        }
        public OrderItem(int productItemId, string mainImage, string productName, decimal price, int quntity)
        {
            ProductItemId = productItemId;
            MainImage = mainImage;
            ProductName = productName;
            Price = price;
            Quntity = quntity;
        }

        public int ProductItemId { get; set; }
        public string MainImage { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quntity { get; set; }
        ///---
        public int OrderId { get; set; }
        [ForeignKey (nameof(OrderId))]
        public virtual Order Order { get; set; }
    }
}

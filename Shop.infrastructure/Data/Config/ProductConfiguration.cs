using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           // builder.Property(x => x.Name).IsRequired();
             builder.Property(x => x.OldPrice).HasColumnType("decimal(18, 2)");
            builder.Property(x => x.NewPrice).HasColumnType("decimal(18, 2)");
            builder.HasData(new Product() { Id = 1, Name = "product1", OldPrice = 113,NewPrice=35, CategoryId = 1,Description= "Description of Product :  1", });
            builder.HasData(new Product() { Id = 2, Name = "product2", OldPrice = 131, NewPrice = 74, CategoryId = 1, Description = "Description of Product :  2", });
            builder.HasData(new Product() { Id = 3, Name = "product3", OldPrice = 133, NewPrice = 35, CategoryId = 1, Description = "Description of Product :  3", });
            builder.HasData(new Product() { Id = 4, Name = "product4", OldPrice = 33, NewPrice = 22, CategoryId = 2, Description = "Description of Product :  4", });
            builder.HasData(new Product() { Id = 5, Name = "product5", OldPrice = 34, NewPrice = 12, CategoryId = 2, Description = "Description of Product :  5", });
            builder.HasData(new Product() { Id = 6, Name = "product6", OldPrice = 64, NewPrice = 55, CategoryId = 2, Description = "Description of Product :  6", });
            builder.HasData(new Product() { Id = 7, Name = "product7", OldPrice = 11, NewPrice = 10, CategoryId = 2, Description = "Description of Product :  7", });
            builder.HasData(new Product() { Id = 8, Name = "product8", OldPrice = 64, NewPrice = 55, CategoryId = 3, Description = "Description of Product :  8", });
            builder.HasData(new Product() { Id = 9, Name = "product9", OldPrice = 64, NewPrice = 55, CategoryId = 3, Description = "Description of Product :  9", });
            builder.HasData(new Product() { Id = 10, Name = "product10", OldPrice = 64, NewPrice = 55, CategoryId = 4, Description = "Description of Product :  10", });
            builder.HasData(new Product() { Id = 11, Name = "product11", OldPrice = 64, NewPrice = 535, CategoryId = 4, Description = "Description of Product :  11", });
            builder.HasData(new Product() { Id = 12, Name = "product12", OldPrice = 64, NewPrice = 255, CategoryId = 4, Description = "Description of Product :  12", });
            builder.HasData(new Product() { Id = 13, Name = "product13", OldPrice = 64, NewPrice = 555, CategoryId = 5, Description = "Description of Product :  13", });
            builder.HasData(new Product() { Id = 14, Name = "product14", OldPrice = 64, NewPrice = 2, CategoryId = 1, Description = "Description of Product :  14", });
            builder.HasData(new Product() { Id = 15, Name = "product15", OldPrice = 64, NewPrice = 5, CategoryId = 1, Description = "Description of Product :  15", });

        }
    }
}

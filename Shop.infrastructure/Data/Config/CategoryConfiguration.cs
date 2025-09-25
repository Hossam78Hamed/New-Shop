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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //builder.Property( x => x.Name).IsRequired().HasMaxLength(30);
            //builder.Property(x => x.Description).IsRequired();
            builder.HasData(new Category { Id= 1,  Name="test1",Description= "Description category1" });
            builder.HasData(new Category { Id = 2, Name = "test2", Description = "Description category2" });
            builder.HasData(new Category { Id = 3, Name = "test3", Description = "Description categor3" });
            builder.HasData(new Category { Id = 4, Name = "test4", Description = "Description category4" });
            builder.HasData(new Category { Id = 5, Name = "test5", Description = "Description category5" });

        }
    }


}


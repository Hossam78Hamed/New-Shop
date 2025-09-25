using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Product;
namespace Shop.infrastructure.Data.Config
{
    internal class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {

        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            //builder.Property( x => x.ImageName).IsRequired();
           // builder.Property(x => x.ProductId).IsRequired();
            builder.HasData(new Photo { Id=1, ImageName ="test1",ProductId=1 });
        }
    
    }
}

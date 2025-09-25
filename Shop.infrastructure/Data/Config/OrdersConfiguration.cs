using Microsoft.EntityFrameworkCore;
using Shop.Domain.Orders;
//using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
//
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Orders; // ✅ Correct namespace for your Order
// Removed StackExchange.Redis unless absolutely needed

//
namespace Shop.infrastructure.Data.Config
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.SubTotal).HasColumnType("decimal(18, 2)");
            builder.Property(x => x.status).HasConversion(o => o.ToString(),
        o => (Status)Enum.Parse(typeof(Status), o));
            //----- 
            builder.OwnsOne(x => x.shippingAddress,
              n => { n.WithOwner(); });

            // // Order -> OrderItems (One-to-Many)

            builder
          .HasMany(o => o.orderItems)
          .WithOne(oi => oi.Order)
          .HasForeignKey(oi => oi.OrderId)
          .OnDelete(DeleteBehavior.Cascade);

            // Order -> DeliveryMethod (Many-to-One)
            builder
                .HasOne(o => o.deliveryMethod)
                .WithMany(dm => dm.Orders)
                .HasForeignKey(o => o.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);



        }
    }
}

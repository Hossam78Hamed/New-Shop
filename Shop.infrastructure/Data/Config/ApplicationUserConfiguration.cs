using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Accounts;
using System.Reflection.Emit;

namespace Shop.infrastructure.Data.Config
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {

        
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder.HasOne(u => u.Address)
                .WithOne(a => a.ApplicationUser)
                .HasForeignKey<Address>(a => a.ApplicationUserId)
                .OnDelete(DeleteBehavior.ClientCascade); // 👈 Automatically deletes Address

            
        }
    }


}



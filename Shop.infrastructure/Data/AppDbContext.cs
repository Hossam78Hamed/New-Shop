using Microsoft.EntityFrameworkCore;
using Shop.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Shop.Domain.Accounts;
using Shop.Domain.Orders;
using Microsoft.AspNetCore.Identity;
//
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Shop.infrastructure.Data
{
    //public class AppDbContext : DbContext
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        public AppDbContext() { }
        public virtual DbSet<Product> Products { set; get; }
        public virtual DbSet<Category> Categorys { set; get; }
        public virtual DbSet<Photo> Photos { set; get; }

        public virtual DbSet<Address> Addresses { set; get; }

        //order

        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<DeliveryMethod> DeliveryMethods { set; get; }
        public virtual DbSet<OrderItem> OrderItems { set; get; }

        

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Shop;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

        //    base.OnConfiguring(optionsBuilder);
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //   => optionsBuilder.UseSqlServer();


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured) // مهم جداً علشان ما يتعارضش مع الـ DI
        //    {
        //        optionsBuilder.UseSqlServer(
        //            "Data Source=DESKTOP-52U8U14;Initial Catalog=Shop;Integrated Security=True;Encrypt=True;Trust Server Certificate=True")
        //            .UseLazyLoadingProxies();
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var entities = ChangeTracker.Entries<BaseEntity<int>>();
            foreach (var entity in entities)
            {
                if
                (entity.State == EntityState.Added)
                {
                    entity.Entity.Created = DateTime.Now;
                  //  entity.Entity.CreatedBy = 1;
                }
                else if (entity.State == EntityState.Modified)
                {
                    if (entity.Entity.IsDeleted == true)
                    {
                        entity.Entity.Deleted = DateTime.Now;
                      //  entity.Entity.DeletedBy = 1;
                    }
                    else
                    {
                        entity.Entity.Updated = DateTime.Now;
                       // entity.Entity.UpdatedBy = 1;
                    }
                }
            }
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity<int>>();
            foreach (var entity in entities)
            {
                if
                (entity.State == EntityState.Added)
                {
                    entity.Entity.Created = DateTime.Now;
                   // entity.Entity.CreatedBy = 1;
                }
                else if (entity.State == EntityState.Modified)
                {
                    if (entity.Entity.IsDeleted == true)
                    {
                        entity.Entity.Deleted = DateTime.Now;
                       // entity.Entity.DeletedBy = 1;
                    }
                    else
                    {
                        entity.Entity.Updated = DateTime.Now;
                        //entity.Entity.UpdatedBy = 1;
                    }
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }



    }
}

/*
2. ApplyConfigurationsFromAssembly(...)

الميثود دي بتخليك تقول للـ EF Core:
"روح شوف أي EntityTypeConfiguration classes
(يعني كلاس بيطبق IEntityTypeConfiguration<T>) 
موجودة في Assembly ده، وطبقها تلقائيًا".
 */
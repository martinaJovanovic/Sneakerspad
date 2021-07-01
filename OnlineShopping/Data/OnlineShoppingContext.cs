using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Areas.Identity.Data;
using OnlineShopping.Models;

namespace OnlineShopping.Data
{
    public class OnlineShoppingContext : IdentityDbContext<OnlineShoppingUser>
    {
        public OnlineShoppingContext(DbContextOptions<OnlineShoppingContext> options)
            : base(options)
        {
        }

        public DbSet<OnlineShopping.Models.Product> Product { get; set; }
        public DbSet<OnlineShopping.Models.Manager> Manager { get; set; }
        public DbSet<OnlineShopping.Models.Client> Client { get; set; }
        public DbSet<OnlineShopping.Models.Size> Size { get; set; }
        public DbSet<OnlineShopping.Models.ProductSize> ProductSize { get; set; }
        public DbSet<OnlineShopping.Models.ShoppingCart> ShoppingCart { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductSize>()
                .HasOne<Size>(p => p.Size)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.SizeId);
            builder.Entity<ProductSize>()
                .HasOne<Product>(p => p.Product)
                .WithMany(p => p.Sizes)
                .HasForeignKey(p => p.ProductId);
            builder.Entity<ShoppingCart>()
                .HasOne<ProductSize>(p => p.ProductSize)
                .WithMany(p => p.Clients)
                .HasForeignKey(p => p.ProductSizeId);
            builder.Entity<ShoppingCart>()
                .HasOne<Client>(p => p.Client)
                .WithMany(p => p.ProductSizes)
                .HasForeignKey(p => p.ClientId);
            base.OnModelCreating(builder);
        }
    }

}
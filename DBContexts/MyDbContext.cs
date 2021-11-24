using ITFAQTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITFAQTest.DBContexts
{
    public class MyDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(p => p.Id).HasName("PK_Product");
            modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique().HasName("Idx_Name");
            modelBuilder.Entity<Product>().Property(p => p.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Name).HasColumnType("nvarchar(200)");
            modelBuilder.Entity<Product>().Property(p => p.Description).HasColumnType("nvarchar(2000)");
            modelBuilder.Entity<Product>().Property(p => p.ImagePath).HasColumnType("nvarchar(500)");
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("double");



            modelBuilder.Entity<CartItem>().ToTable("CartItems");
            modelBuilder.Entity<CartItem>().HasKey(c => c.Id).HasName("PK_CartItems");
            modelBuilder.Entity<CartItem>().Property(c => c.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<CartItem>().Property(c => c.ProductId).HasColumnType("int");
            modelBuilder.Entity<CartItem>().Property(c => c.Quantity).HasColumnType("int");
            modelBuilder.Entity<CartItem>().Property(c => c.Amount).HasColumnType("double");

            modelBuilder.Entity<CartItem>().HasOne<Product>().WithMany().HasPrincipalKey(p => p.Id).HasForeignKey(c => c.ProductId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_CartItems_Products");

        }
    }
}
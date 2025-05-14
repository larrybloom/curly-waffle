using ECommerce.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Product entity
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            // Configure CartItem entity
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId);

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic devices and gadgets" },
                new Category { Id = 2, Name = "Clothing", Description = "Apparel and fashion items" },
                new Category { Id = 3, Name = "Books", Description = "Books and publications" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Latest smartphone with advanced features",
                    Price = 699.99m,
                    CategoryId = 1,
                    Stock = 50
                },
                new Product
                {
                    Id = 2,
                    Name = "Laptop",
                    Description = "Powerful laptop for work and gaming",
                    Price = 1299.99m,
                    CategoryId = 1,
                    Stock = 30
                },
                new Product
                {
                    Id = 3,
                    Name = "T-Shirt",
                    Description = "Comfortable cotton t-shirt",
                    Price = 24.99m,
                    CategoryId = 2,
                    Stock = 100
                },
                new Product
                {
                    Id = 4,
                    Name = "Jeans",
                    Description = "Classic blue jeans",
                    Price = 49.99m,
                    CategoryId = 2,
                    Stock = 75
                },
               new Product
               {
                   Id = 5,
                   Name = "Programming Guide",
                   Description = "Comprehensive programming guide for beginners",
                   Price = 39.99m,
                   CategoryId = 3,
                   Stock = 25
               },
                new Product
                {
                    Id = 6,
                    Name = "Novel",
                    Description = "Bestselling fiction novel",
                    Price = 19.99m,
                    CategoryId = 3,
                    Stock = 40
                }
  );
        }





    }
}

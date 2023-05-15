using FlowerShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Data
{
    public class FlowerShopDbContext : DbContext
    {
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Merchandise> Merchandises { get; set; }
        public DbSet<MerchandiseCategory> MerchandiseCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLog> OrderLogs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your database connection string here
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Database=FlowerShopSystem;Trusted_Connection=True");
        }
    }
}

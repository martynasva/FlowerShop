using FlowerShop.Models;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

// Temporary DataContext, we might not need to save everything to database.
namespace FlowerShop.Data
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions options) : base(options) 
        {  
        }
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
    }
}

using FlowerShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


// Temporary DataContext, we might not need to save everything to database.
namespace FlowerShop.Data
{
        public class DataContext : IdentityDbContext<AppUser, AppRole, Guid,
         IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, 
         IdentityUserToken<Guid>> 
    {
        
        public DataContext(DbContextOptions options) : base(options) 
        {  
            
        }

       // public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
       // public DbSet<Discount> Discounts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Merchandise> Merchandises { get; set; }
        public DbSet<MerchandiseCategory> MerchandiseCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLog> OrderLogs { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}

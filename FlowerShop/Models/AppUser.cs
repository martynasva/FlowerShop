using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FlowerShop.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Address { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
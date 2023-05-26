using Microsoft.AspNetCore.Identity;

namespace FlowerShop.Models
{
    public class AppRole : IdentityRole<Guid>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
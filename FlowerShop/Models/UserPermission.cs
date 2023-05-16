using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class UserPermission
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string PermissionName { get; set; }

        public string? PermissionDescription { get; set; }

        public virtual List<UserType> UserTypes { get; set; }
    }
}

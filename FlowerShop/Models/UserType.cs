using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class UserType
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual UserPermission UserPermissions { get; set; }
    }
}

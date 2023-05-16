using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class User
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Required]
        public Guid UserTypeID { get; set; }
        public virtual UserType UserType { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}

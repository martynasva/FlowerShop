using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class Discount
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public virtual Merchandise Merchandise { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime ActiveUntil { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public decimal? DiscountAmmount { get; set; }

        public virtual ICollection<User> ApplicableUsers { get; set; }
    }
}

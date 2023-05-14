using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class Merchandise
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<MerchandiseCategory> Categories { get; set; }
    }
}

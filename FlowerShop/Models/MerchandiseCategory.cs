using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class MerchandiseCategory
    {
        [Key]
        public Guid ID { get; set; }

        public virtual ICollection<MerchandiseCategory> ParentCategory { get; set; }

        [Required]
        public string Name;
    }
}

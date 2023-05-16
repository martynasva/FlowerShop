using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class MerchandiseCategory
    {
        [Key]
        public Guid ID { get; set; }

        public Guid? ParentCategoryID { get; set; }

        public virtual MerchandiseCategory ParentCategory { get; set; }

        [Required]
        public string Name;

        public virtual ICollection<Merchandise> Merchandises { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class Item
    {
        [Key]
        public Guid ID { get; set; }   
           
        [Required]
        public Guid MerchandiceID { get; set; }

        public virtual Merchandise Merchandise { get; set; }

        public virtual Order Order { get; set; }

        public string? CountryOfOrigin { get; set; }

        public DateTime? DateOfManufacture { get; set; }
    }
}

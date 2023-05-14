using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class Delivery
    {
        [Key]
        public Guid ID { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string? DeliveryType { get; set; }

        public string DeliveryOptions { get; set; }
    }
}

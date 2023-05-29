using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class OrderLog
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public Guid OrderID { get; set; }

        public string? LogText { get; set; }

        [Required]
        public DateTime? Timestamp { get; set; }
    }
}

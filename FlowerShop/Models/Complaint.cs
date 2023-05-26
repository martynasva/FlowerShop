using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class Complaint
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public virtual AppUser User { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        public string ComplaintText { get; set; }
    }
}

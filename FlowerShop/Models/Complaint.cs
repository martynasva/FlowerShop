using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public class Complaint
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        public string ComplaintText { get; set; }
    }
}

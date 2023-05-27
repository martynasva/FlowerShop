using FlowerShop.Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowerShop.Models
{
    public class Payment
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string PaymentType { get; set; }

        [Required]
        public virtual Order Order { get; set; }


        [Column("PaymentStatus")]
        public string PaymentStatusString
        {
            get { return PaymentStatus.ToString(); }
            private set { PaymentStatus = value.ParseEnum<PaymentStatus>(); }
        }

        [NotMapped]
        public PaymentStatus PaymentStatus { get; set; }
    }

    public enum PaymentStatus
    {
        Pending = 1,
        BankApprovePending = 2,
        Succeeded = 3,
        Failed = 3,
        Rejected = 4,
        Refunded = 5
    }
}

using FlowerShop.Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowerShop.Models
{
    public class Order
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public virtual User User { get; set; }

        public virtual Delivery Delivery { get; set; }

        public virtual ICollection<Item> Items { get; set; }


        [Column("OrderStatus")]
        public string OrderStatusString
        {
            get { return OrderStatus.ToString(); }
            private set { OrderStatus = value.ParseEnum<OrderStatus>(); }
        }

        [NotMapped]
        public OrderStatus OrderStatus { get; set; }
    }

    public enum OrderStatus
    {
        Cart = 1,
        Pending = 2,
        Paid = 3,
        Delivered = 3,
        Canceled = 4,
        Refunded = 5
    }
}

﻿using FlowerShop.Utility;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowerShop.Models
{
    public class Order
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        public virtual AppUser User { get; set; }

        public virtual Delivery Delivery { get; set; }

        public virtual ICollection<Item> Items { get; } = new List<Item>();


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

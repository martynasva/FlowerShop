using FlowerShop.Models;
using FlowerShop.Utility;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.DTOs
{
    public class OrderDTO
    {
        public Guid ID { get; set; }

        public Guid UserID { get; set; }

        public string OrderStatusString { get; set; }

        public static OrderDTO FromOrder(Order order)
        {
            return new OrderDTO
            {
                ID = order.ID,
                UserID = order.UserID,
/*                DeliveryID = order.Delivery.ID,
                ItemIDs = order.Items.Select(item => item.ID).ToArray(),*/
                OrderStatusString = order.OrderStatus.ToString()
            };
        }
        public static Order ToOrder(OrderDTO orderDTO)
        {
            return new Order
            {
                ID = orderDTO.ID,
                UserID = orderDTO.UserID,
                OrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderDTO.OrderStatusString)
            };
        }
    }
}

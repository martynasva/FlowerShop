using FlowerShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.DTOs
{
    public class CreateOrderDTO
    {

        [Required]
        public Guid UserID { get; set; }

        public string OrderStatusString { get; set; }

        public static Order ToOrder(CreateOrderDTO createOrderDTO)
        {
            return new Order
            {
                ID = Guid.NewGuid(),
                OrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), createOrderDTO.OrderStatusString),
            };
        }
    }
}

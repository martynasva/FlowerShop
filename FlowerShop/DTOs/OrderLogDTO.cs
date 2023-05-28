using FlowerShop.Models;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.DTOs
{
    public class OrderLogDTO
    {
        public Guid ID { get; set; }

        public Guid? OrderID { get; set; }

        public string? LogText { get; set; }

        public DateTime? Timestamp { get; set; }

        public static OrderLogDTO FromOrderLog(OrderLog orderLog)
        {
            return new OrderLogDTO
            {
                ID = orderLog.ID,
                OrderID = orderLog.OrderID,
                LogText = orderLog.LogText,
                Timestamp = orderLog.Timestamp,
            };
        }
    }
}

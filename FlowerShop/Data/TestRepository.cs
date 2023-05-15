using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowerShop.Interfaces;
using FlowerShop.Models;

//Temporary Repository to check if database works. To be deleted later.
namespace FlowerShop.Data
{
    public class TestRepository : ITestRepository
    {
        private readonly DataContext  _dataContext;

        public TestRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void SeedData()
        {
            var deliveries = new List<Delivery>
            {
                new Delivery { ID = Guid.NewGuid(), DeliveryDate = DateTime.UtcNow.AddDays(1), DeliveryType = "Express", DeliveryOptions = "Option 1, Option 2" },
                new Delivery { ID = Guid.NewGuid(), DeliveryDate = DateTime.UtcNow.AddDays(2), DeliveryType = "Standard", DeliveryOptions = "Option 3, Option 4" },
            };
            _dataContext.Deliveries.AddRange(deliveries);

            _dataContext.SaveChanges();
        }

        public void PrintData()
        {
            var deliveries = _dataContext.Deliveries.ToList();
            foreach (var delivery in deliveries)
            {
                Console.WriteLine($"ID: {delivery.ID}");
                Console.WriteLine($"Delivery Date: {delivery.DeliveryDate}");
                Console.WriteLine($"Delivery Type: {delivery.DeliveryType}");
                Console.WriteLine($"Delivery Options: {delivery.DeliveryOptions}");
                Console.WriteLine();
            }
        }

    }

}
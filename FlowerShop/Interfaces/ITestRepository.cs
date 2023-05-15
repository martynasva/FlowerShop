using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerShop.Interfaces
{
    public interface ITestRepository
    {
        public void SeedData();

        public void PrintData();
    }
}
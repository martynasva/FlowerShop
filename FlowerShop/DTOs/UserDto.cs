using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerShop.DTOs
{
    public class UserDto
    {
        public string Username { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Token { get; set; }
    }
}
using FlowerShop.Models;

namespace FlowerShop.DTOs
{
    public class UserDto
    {
        public string Username { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Token { get; set; }
        public Guid Id { get; set; }

        public static UserDto FromUser(AppUser user)
        {
            return new UserDto
            {
                Username = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                Id = user.Id
            };
        }
    }
}
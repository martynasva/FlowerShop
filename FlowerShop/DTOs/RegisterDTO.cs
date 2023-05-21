using System.ComponentModel.DataAnnotations;

namespace FlowerShop.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [StringLength(24, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
    }
}
using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
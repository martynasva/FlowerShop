using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IUserRepository
    
    {
        void Update(AppUser user);
        Task<IEnumerable<AppUser>> GetUserAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
    }
}
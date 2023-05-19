using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowerShop.Data;
using FlowerShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Repositories
{
    public class UserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IEnumerable<AppUser>> GetUserAsync()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _dataContext.Users.FindAsync(id);
        }
        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(x => x.UserName == username);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
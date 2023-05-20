using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowerShop.DTOs;
using FlowerShop.Extensions;
using FlowerShop.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

         public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;        
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        { 
            var users = await _userRepository.GetUsersAsync();
            var userDtos = users.Select(UserDto.FromUser);
            return Ok(userDtos);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            
            if(user == null) return NotFound();

            user.Address = userUpdateDto.Address;
            user.Name = userUpdateDto.Name;
            user.Surname = userUpdateDto.Surname;

            if(await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }
    }
}
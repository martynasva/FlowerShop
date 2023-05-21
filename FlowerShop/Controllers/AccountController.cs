using FlowerShop.DTOs;
using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")] //POST: api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDTO registerDto)
        {
            if(await UserExists(registerDto.Username)) return BadRequest();

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                Name = registerDto.Name,
                Surname = registerDto.Surname,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            
            if(!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            
            if(!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                Username = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                Token = await _tokenService.CreateToken(user),
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDTO loginDto)
        {
            var user = await _userManager.Users
            .SingleOrDefaultAsync(x =>x.UserName == loginDto.Username);
            
            if(user == null) return Unauthorized("Invalid username");
            
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if(!result) return Unauthorized("Invalid password");

            return new UserDto
            {
                Username = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                Token = await _tokenService.CreateToken(user),
            };

        }
        
        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x=>x.UserName == username.ToLower());
        }
    }
}
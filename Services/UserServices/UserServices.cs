using Core.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Services.TokenServices;
using Services.UserServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _token;

        public UserServices(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenServices token)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
        }

        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return null;
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password , false);

            if (!result.Succeeded)
                return null;
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _token.CreateToken(user)
            };
        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null)
                return null;

            var appUser = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email.Split("@")[0]
            };
            var result = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (!result.Succeeded)
                return null;

            return new UserDto
            {
                DisplayName = appUser.DisplayName,
                Email = appUser.Email,
                Token = _token.CreateToken(appUser)
            };

        }
    }
}

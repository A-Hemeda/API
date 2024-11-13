using Core.Entities.IdentityEntities;
using Demo.API.HandleResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.UserServices;
using Services.UserServices.Dto;
using System.Security.Claims;

namespace Demo.API.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserServices userServices , UserManager<AppUser> userManager)
        {
            _userServices = userServices;
            _userManager = userManager;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userServices.Register(registerDto);

            if (user == null)
                return BadRequest(new ApiException(400, "Email Already Exist"));

            return Ok(user);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userServices.Login(loginDto);

            if (user == null)
                return Unauthorized(new ApiResponse(401));

            return Ok(user);
        }
        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
            };
        }
    }
}
using Services.UserServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserServices
{
    public interface IUserServices
    {
        Task<UserDto> Register(RegisterDto registerDto); 
        Task<UserDto> Login(LoginDto loginDto); 
    }
}

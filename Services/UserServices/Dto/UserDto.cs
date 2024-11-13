using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserServices.Dto
{
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }

        public string Token { get; set; }
    }
}

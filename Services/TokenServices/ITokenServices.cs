using Core.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TokenServices
{
    public interface ITokenServices
    {
        public string CreateToken(AppUser appUser);
    }
}

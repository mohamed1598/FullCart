using FullCart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user,string role);
    }
}

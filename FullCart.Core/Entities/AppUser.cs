using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Core.Entities
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; } = null!;
        public Address? Address { get; set; }
    }
}

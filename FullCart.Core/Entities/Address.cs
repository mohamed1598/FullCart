using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Core.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        [Required]
        public string AppUserId { get; set; } = null!;
        public AppUser? AppUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Core.Entities
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
    }
}

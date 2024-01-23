﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Core.Entities
{
    public class Cart : BaseEntity
    {
        public string Status { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public AppUser? User { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
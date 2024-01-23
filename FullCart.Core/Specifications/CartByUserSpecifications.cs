using FullCart.Core.Consts;
using FullCart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Core.Specifications
{
    public class CartByUserSpecifications : BaseSpecifications<Cart>
    {
        public CartByUserSpecifications(string userId) : base(x => x.UserId == userId && x.Status == CartStatus.Entered)
        {
            AddInclude(x => x.CartItems);
        }
    }
}
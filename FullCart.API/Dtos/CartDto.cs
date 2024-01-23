using FullCart.Core.Entities;

namespace FullCart.API.Dtos
{
    public class CartDto
    {
        public int Id { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
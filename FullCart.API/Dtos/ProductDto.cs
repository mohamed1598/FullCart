using FullCart.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FullCart.API.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;

        public int BrandId { get; set; }
        public string Brand { get; set; } = null!;
        public int CategoryId { get; set; }
        public string Category { get; set; } = null!;
    }
}
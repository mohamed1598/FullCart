using System.ComponentModel.DataAnnotations;

namespace FullCart.API.Dtos
{
    public class BrandDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string PictureUrl { get; set; } = null!;
    }
}

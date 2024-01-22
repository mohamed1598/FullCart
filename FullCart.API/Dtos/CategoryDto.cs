using System.ComponentModel.DataAnnotations;

namespace FullCart.API.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
    }
}

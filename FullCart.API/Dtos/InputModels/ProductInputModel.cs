using System.ComponentModel.DataAnnotations;

namespace FullCart.API.Dtos.InputModels
{
    public class ProductInputModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public IFormFile? Picture { get; set; }
        public bool? RemoveImage { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int CategoryId { get; set; }

    }
}
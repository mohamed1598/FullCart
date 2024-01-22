using System.ComponentModel.DataAnnotations;

namespace FullCart.API.Dtos.InputModels
{
    public class BrandInputModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public IFormFile? Picture { get; set; }
        public bool? RemoveImage { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FullCart.API.Dtos.InputModels
{
    public class BrandInputModel
    {
        public string Name { get; set; } = null!;
        public IFormFile? Picture { get; set; }
    }
}

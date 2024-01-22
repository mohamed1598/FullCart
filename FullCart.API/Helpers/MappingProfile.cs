using AutoMapper;
using FullCart.API.Dtos;
using FullCart.API.Dtos.InputModels;
using FullCart.Core.Entities;

namespace FullCart.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, AppUser>()
     .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<AppUser, UserDto>();

            CreateMap<BrandInputModel, Brand>();
            CreateMap<Brand, BrandDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryInputModel, Category>();

            CreateMap<Product, ProductDto>()
             .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
             .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<ProductInputModel, Product>();
        }
    }
}
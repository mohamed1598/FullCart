using AutoMapper;
using FullCart.API.Dtos;
using FullCart.Core.Entities;

namespace FullCart.API.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<AppUser, UserDto>();
        }
    }
}

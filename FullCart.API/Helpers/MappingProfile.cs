using AutoMapper;
using FullCart.API.Dtos;
using FullCart.Core.Entities;

namespace FullCart.API.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, UserDto>();
        }
    }
}

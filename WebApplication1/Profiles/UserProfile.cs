using AutoMapper;
using WebApplication1.DTO;
using WebApplication1.Entity;
using WebApplication1.Services;

namespace WebApplication1.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}

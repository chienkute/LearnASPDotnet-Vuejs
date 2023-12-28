using AutoMapper;
using DatingApp.API.Databases.Entities;
using DatingApp.API.DTOs;

namespace DatingApp.API.Profiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, MemberDto>()
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.GetAge())
                )
                .ForMember(
                    dest => dest.UserTypeName,
                    opt => opt.MapFrom(src => src.UserType.Name)
                );
        }
    }
}
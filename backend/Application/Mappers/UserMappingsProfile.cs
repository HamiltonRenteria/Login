using Application.DTOs.User.Request;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class UserMappingsProfile : Profile
    {
        public UserMappingsProfile()
        {
            _ = CreateMap<UserRequestDto, User>();
            _ = CreateMap<TokenRequestDto, User>();
        }
    }
}

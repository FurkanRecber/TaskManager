using AutoMapper;
using TaskManager.Application.Abstraction.src.User.Dto;
using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Application.src.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, RegisterDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}
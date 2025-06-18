using AutoMapper;
using TaskManager.Application.Abstraction.Dto;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.src.Mapping;

public class TagMapping : Profile
{
    public TagMapping()
    {
        CreateMap<Tag, TagDto>().ReverseMap();
        CreateMap<Tag, CreateTagDto>().ReverseMap();
    }
}
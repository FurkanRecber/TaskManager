using AutoMapper;
using TaskManager.Application.Abstraction.Dto;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.src.Mapping;

public class CommentMapping : Profile
{
    public CommentMapping()
    {
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Comment, CreateCommentDto>().ReverseMap();
    }
}
using TaskManager.Application.Abstraction.Dto;
using TaskManager.Application.src.Abstraction.Base;
using TaskManager.Common.Models.Response;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstraction;

public interface ITagService : ICrudService<Tag, TagDto>
{
    Task<ServiceResponse<NoContent>> Create(CreateTagDto model);
}
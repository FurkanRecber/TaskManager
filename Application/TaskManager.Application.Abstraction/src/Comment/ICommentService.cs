using TaskManager.Application.Abstraction.Dto;
using TaskManager.Application.src.Abstraction.Base;
using TaskManager.Common.Models.Response;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Abstraction;

public interface ICommentService : ICrudService<Comment, CommentDto>
{
    Task<ServiceResponse<List<CommentDto>>> Get();
    Task<ServiceResponse<NoContent>> Create(CreateCommentDto model);
}
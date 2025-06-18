using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstraction;
using TaskManager.Application.Abstraction.Dto;
using TaskManager.Application.Abstraction.src;
using TaskManager.Application.src.Base;
using TaskManager.Common.Models.Response;
using TaskManager.Domain.Entities;
using TaskManager.Persistence.Repository;
using TaskManager.Persistence.UnitOfWork;

namespace TaskManager.Application;

public class CommentService(
    IRepository<Comment> repository,
    IMapper mapper,
    IUserService userService,
    IUnitOfWork unitOfWork) 
    : CrudService<Comment, CommentDto>(repository, mapper, unitOfWork), ICommentService
{
    public async Task<ServiceResponse<List<CommentDto>>> Get()
    {
        var comments = await repository
            .GetQueryable()
            .Include(x => x.User)
            .ToListAsync();
        var dto = mapper.Map<List<CommentDto>>(comments);
        return ServiceResponse<List<CommentDto>>.Success(dto, StatusCodes.Status200OK);
    }
    public async Task<ServiceResponse<NoContent>> Create(CreateCommentDto model)
    {
        var comment = mapper.Map<Comment>(model);
        comment.UserId = userService.GetCurrentUserId();
        await repository.CreateAsync(comment);
        await unitOfWork.CommitAsync();
        return ServiceResponse<NoContent>.Success(StatusCodes.Status201Created);
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TaskManager.Application.Abstraction;
using TaskManager.Application.Abstraction.Dto;
using TaskManager.Application.src.Base;
using TaskManager.Common.Models.Response;
using TaskManager.Domain.Entities;
using TaskManager.Persistence.Repository;
using TaskManager.Persistence.UnitOfWork;

namespace TaskManager.Application;

public class TagService(
    IRepository<Tag> repository,
    IMapper mapper,
    IUnitOfWork unitOfWork) 
    : CrudService<Tag, TagDto>(repository, mapper, unitOfWork), ITagService
{
    public async Task<ServiceResponse<NoContent>> Create(CreateTagDto model)
    {
        var tag = mapper.Map<Tag>(model);
        await repository.CreateAsync(tag);
        await unitOfWork.CommitAsync();
        return ServiceResponse<NoContent>.Success(StatusCodes.Status201Created);
    }
}
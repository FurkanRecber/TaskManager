using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using TaskManager.Application.src.Abstraction.Base;
using TaskManager.Common.Exceptions;
using TaskManager.Common.Models.Dtos;
using TaskManager.Common.Models.Entities;
using TaskManager.Common.Models.Response;
using TaskManager.Persistence.Repository;
using TaskManager.Persistence.UnitOfWork;

namespace TaskManager.Application.src.Base;

public class CrudService<T, TDto>(IRepository<T> repository, IMapper mapper, IUnitOfWork unitOfWork) //dependency injections
    : ICrudService<T, TDto> //implementations
    where T : BaseEntity where TDto : BaseDto //constraints
{ 
    public async Task<ServiceResponse<List<TDto>>> GetListAsync(Expression<Func<T?, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IQueryable<T>>? includeProperties = null,
        bool disableTracking = true)
    {
        var list = await repository.GetListAsync(predicate, orderBy, includeProperties, disableTracking);
        var dto = mapper.Map<List<TDto>>(list);
        return ServiceResponse<List<TDto>>.Success(dto, StatusCodes.Status200OK);
    }
    public async Task<ServiceResponse<TDto>> GetFirstOrDefaultAsync(Expression<Func<T?, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IQueryable<T>>? includeProperties = null,
        bool disableTracking = true)
    {
        var entity = await repository.GetFirstOrDefaultAsync(predicate, orderBy, includeProperties, disableTracking);
        if(entity == null) throw new NotFoundException(typeof(T).Name + " Not Found");
        var dto = mapper.Map<TDto>(entity);
        return ServiceResponse<TDto>.Success(dto, StatusCodes.Status200OK);
    }
    public async Task<ServiceResponse<List<TDto>>> GetPagedListAsync(int page, int size, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IQueryable<T>>? includeProperties = null, bool disableTracking = true)
    {
        var list = await repository.GetPagedListAsync(page, size, predicate, orderBy, includeProperties, disableTracking);
        var dto = mapper.Map<List<TDto>>(list);
        return ServiceResponse<List<TDto>>.Success(dto, StatusCodes.Status200OK);
    }
    public async Task<ServiceResponse<TDto>> CreateAsync(TDto dto)
    {
        dto.Id = Guid.NewGuid();
        await repository.CreateAsync(mapper.Map<T>(dto));
        await unitOfWork.CommitAsync();
        return ServiceResponse<TDto>.Success(dto, StatusCodes.Status201Created);
    }
    public async Task<ServiceResponse<TDto>> UpdateAsync(TDto dto)
    {
        var entity = await repository.GetFirstOrDefaultAsync(x => x.Id == dto.Id);
        if (entity == null) throw new NotFoundException(typeof(T).Name + " Not Found");
        entity = mapper.Map(dto, entity);
        repository.Update(entity);
        await unitOfWork.CommitAsync();
        return ServiceResponse<TDto>.Success(dto, StatusCodes.Status200OK);
    }
    public async Task<ServiceResponse<NoContent>> DeleteAsync(Guid id)
    {
        var entity = await repository.GetFirstOrDefaultAsync(x => x.Id == id);
        if(entity == null) throw new NotFoundException(typeof(T).Name + " Not Found");
        repository.Delete(entity);
        await unitOfWork.CommitAsync();
        return ServiceResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
}
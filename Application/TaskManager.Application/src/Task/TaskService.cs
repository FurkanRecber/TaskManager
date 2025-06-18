using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstraction;
using TaskManager.Application.Abstraction.Dto;
using TaskManager.Application.Abstraction.src;
using TaskManager.Application.src.Base;
using TaskManager.Common.Exceptions;
using TaskManager.Common.Models.Response;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Entities.Identity;
using TaskManager.Persistence.Repository;
using TaskManager.Persistence.UnitOfWork;
using Task = TaskManager.Domain.Entities.Task;
using TaskStatus = TaskManager.Common.Models.Enums.TaskStatus;

namespace TaskManager.Application;

public class TaskService(
    IRepository<Task> repository,
    IRepository<Tag> tagRepository,
    IHttpContextAccessor httpContextAccessor,
    IUserService userService,
    UserManager<User> userRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) 
    : CrudService<Task, TaskDto>(repository, mapper, unitOfWork), ITaskService
{
    public async Task<ServiceResponse<List<TaskDto>>> GetAll()
    {
        var tasks = await repository.GetQueryable()
            .Include(x => x.User)
            .Include(x => x.Comments)
                .ThenInclude(x => x.User)
            .Include(x => x.Tags)
            .ToListAsync();

        var dto = mapper.Map<List<TaskDto>>(tasks);
        return ServiceResponse<List<TaskDto>>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<List<TaskDto>>> GetByUser(Guid userId)
    {
        if (!await userRepository.Users.AnyAsync(x => x.Id == userId))
            throw new NotFoundException("User not found");
        
        var tasks = await repository.GetQueryable()
            .Include(x => x.User)
            .Include(x => x.Comments).ThenInclude(x => x.User)
            .Include(x => x.Tags)
            .Where(x => x.UserId == userId)
            .ToListAsync();
        
        var dto = mapper.Map<List<TaskDto>>(tasks);
        return ServiceResponse<List<TaskDto>>.Success(dto, StatusCodes.Status200OK);
    }
    
    public async Task<ServiceResponse<List<TaskDto>>> GetByTag(Guid tagId)
    {
        if (!await tagRepository.GetQueryable().AnyAsync(x => x.Id == tagId))
            throw new NotFoundException("Tag not found");
        
        var tasks = await repository.GetQueryable()
            .Include(x => x.User)
            .Include(x => x.Comments).ThenInclude(x => x.User)
            .Include(x => x.Tags)
            .Where(x => x.Tags.Any(x => x.Id == tagId))
            .ToListAsync();
        
        var dto = mapper.Map<List<TaskDto>>(tasks);
        return ServiceResponse<List<TaskDto>>.Success(dto, StatusCodes.Status200OK);
    }
    
    public async Task<ServiceResponse<List<TaskDto>>> GetByState(TaskStatus status)
    {
        var tasks = await repository.GetQueryable()
            .Include(x => x.User)
            .Include(x => x.Comments).ThenInclude(x => x.User)
            .Include(x => x.Tags)
            .Where(x => x.Status == status)
            .ToListAsync();
        
        var dto = mapper.Map<List<TaskDto>>(tasks);
        return ServiceResponse<List<TaskDto>>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<List<TaskDto>>> GetByDate(int days)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(days);

        var userId = userService.GetCurrentUserId();
        var tasks = await repository.GetQueryable()
            .Include(x => x.User)
            .Include(x => x.Comments).ThenInclude(x => x.User)
            .Include(x => x.Tags)
            .Where(x => x.DueDate >= DateTime.Now.AddDays(-days) && x.UserId == userId)
            .ToListAsync();
        
        var dto = mapper.Map<List<TaskDto>>(tasks);
        return ServiceResponse<List<TaskDto>>.Success(dto, StatusCodes.Status200OK);
    }
    
    public async Task<ServiceResponse<NoContent>> AddTag(Guid id, Guid tagId)
    {
        var task = await repository.GetQueryable()
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (task == null) throw new NotFoundException("Task not found");
        
        var tag = await tagRepository.GetFirstOrDefaultAsync(x => x.Id == tagId);
        if (tag == null) throw new NotFoundException("Tag not found");
        
        task.Tags.Add(tag);
        repository.Update(task);
        await unitOfWork.CommitAsync();
        return ServiceResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
    
    public async Task<ServiceResponse<List<TaskDto>>> UpdateState(Guid id, TaskStatus status)
    {
        var task = await repository.GetQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();
        if (task == null) throw new NotFoundException("Task not found");
        task.Status = status;
        var dto = mapper.Map<List<TaskDto>>(task);
        return ServiceResponse<List<TaskDto>>.Success(dto, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<NoContent>> Create(CreateTaskDto model)
    {
        if (model.DueDate < DateTime.Now) throw new BadRequestException("Due date cannot be in the past");
        var task = mapper.Map<Task>(model);
        task.Id = Guid.NewGuid();
        task.UserId = userService.GetCurrentUserId();
        task.Status = TaskStatus.TODO;
        await repository.CreateAsync(task);
        await unitOfWork.CommitAsync();
        return ServiceResponse<NoContent>.Success(StatusCodes.Status201Created);
    }
}
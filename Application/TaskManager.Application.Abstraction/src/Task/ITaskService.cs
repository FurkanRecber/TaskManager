using TaskManager.Application.Abstraction.Dto;
using TaskManager.Application.src.Abstraction.Base;
using TaskManager.Common.Models.Response;
using Task = TaskManager.Domain.Entities.Task;
using TaskStatus = TaskManager.Common.Models.Enums.TaskStatus;

namespace TaskManager.Application.Abstraction;

public interface ITaskService : ICrudService<Task, TaskDto>
{
    Task<ServiceResponse<List<TaskDto>>> GetAll();
    Task<ServiceResponse<List<TaskDto>>> GetByTag(Guid tagId);
    Task<ServiceResponse<List<TaskDto>>> GetByState(TaskStatus status);
    Task<ServiceResponse<List<TaskDto>>> GetByUser(Guid userId);
    Task<ServiceResponse<List<TaskDto>>> GetByDate(int days);
    Task<ServiceResponse<NoContent>> AddTag(Guid id, Guid tagId);
    Task<ServiceResponse<List<TaskDto>>> UpdateState(Guid id, TaskStatus status);
    Task<ServiceResponse<NoContent>> Create(CreateTaskDto model);
}
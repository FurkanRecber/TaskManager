using AutoMapper;
using TaskManager.Application.Abstraction.Dto;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.src.Mapping;

public class TaskMapping : Profile
{
    public TaskMapping()
    {
        CreateMap<Task, TaskDto>().ReverseMap();
        CreateMap<CreateTaskDto, Task>().ReverseMap();
    }
}
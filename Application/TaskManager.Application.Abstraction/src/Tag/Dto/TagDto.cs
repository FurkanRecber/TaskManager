using TaskManager.Application.Abstraction.src.User.Dto;
using TaskManager.Common.Models.Dtos;
using TaskManager.Common.Models.Enums;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace TaskManager.Application.Abstraction.Dto;

public class TagDto : BaseDto
{
    public string Name { get; set; } = null!;
    public List<TaskDto> Tasks { get; set; } = new();
}
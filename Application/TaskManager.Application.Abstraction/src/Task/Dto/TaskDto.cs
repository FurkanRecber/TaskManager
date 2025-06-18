using TaskManager.Application.Abstraction.src.User.Dto;
using TaskManager.Common.Models.Dtos;
using TaskManager.Common.Models.Enums;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace TaskManager.Application.Abstraction.Dto;

public class TaskDto : BaseDto
{
    //Foreign Keys
    public Guid UserId { get; set; }

    //Properties
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public TaskStatus Status { get; set; }
    public Priority Priority { get; set; }
    
    //Navigation Properties
    public UserDto? User { get; set; }
    public List<CommentDto> Comments { get; set; } = new();
    public List<TagDto> Tags { get; set; } = new();
}
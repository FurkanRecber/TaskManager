using TaskManager.Application.Abstraction.src.User.Dto;
using TaskManager.Common.Models.Dtos;

namespace TaskManager.Application.Abstraction.Dto;

public class CommentDto : BaseDto
{
    //Foreign Keys
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
    
    public string Content { get; set; } = null!;
    
    //Navigation Properties
    public UserDto? User { get; set; }
    public TaskDto? Task { get; set; }
}
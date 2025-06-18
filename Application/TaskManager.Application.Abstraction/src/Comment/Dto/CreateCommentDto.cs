namespace TaskManager.Application.Abstraction.Dto;

public class CreateCommentDto
{
    public Guid TaskId { get; set; }
    public string Content { get; set; } = null!;
}
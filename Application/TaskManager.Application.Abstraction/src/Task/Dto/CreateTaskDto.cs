using TaskManager.Common.Models.Enums;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace TaskManager.Application.Abstraction.Dto;

public class CreateTaskDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public TaskStatus Status { get; set; }
    public Priority Priority { get; set; }
}
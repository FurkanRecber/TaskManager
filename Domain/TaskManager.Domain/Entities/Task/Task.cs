using TaskManager.Common.Models.Entities;
using TaskManager.Common.Models.Enums;
using TaskManager.Domain.Entities.Identity;
using TaskStatus = TaskManager.Common.Models.Enums.TaskStatus;


namespace TaskManager.Domain.Entities;

public class Task : BaseEntity
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
    public User User { get; set; }
    public List<Comment> Comments { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();
}
using TaskManager.Common.Models.Entities;

namespace TaskManager.Domain.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = null!;
    public List<Task> Tasks { get; set; } = new();
}
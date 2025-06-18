using TaskManager.Common.Models.Entities;
using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Domain.Entities;

public class Comment : BaseEntity
{
    //Foreign Keys
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
    
    public string Content { get; set; } = null!;
    
    //Navigation Properties
    public User? User { get; set; }
    public Task? Task { get; set; }
}
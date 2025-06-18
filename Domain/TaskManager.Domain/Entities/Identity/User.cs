using Microsoft.AspNetCore.Identity;

namespace TaskManager.Domain.Entities.Identity;

public class User : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }

    public List<Task> Tasks { get; set; } = new();
}
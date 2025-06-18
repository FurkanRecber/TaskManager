using TaskManager.Common.Models.Token;
using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Infrastructure;

public interface ITokenHandler
{
    Token CreateToken(User user);
}
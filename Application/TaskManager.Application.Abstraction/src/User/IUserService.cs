using TaskManager.Application.Abstraction.src.User.Dto;
using TaskManager.Common.Models.Response;
using TaskManager.Common.Models.Token;

namespace TaskManager.Application.Abstraction.src;

public interface IUserService
{
    Task<ServiceResponse<Token>> Login(LoginDto dto);
    Task<ServiceResponse<Token>> LoginWithRefreshToken(string refreshToken);
    Task<ServiceResponse> Register(RegisterDto model);
    Guid GetCurrentUserId();
}
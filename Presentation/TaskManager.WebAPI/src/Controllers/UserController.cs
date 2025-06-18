using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Abstraction.src;
using TaskManager.Application.Abstraction.src.User.Dto;
using TaskManager.WebAPI.Controllers.Base;

namespace TaskManager.WebAPI.Controllers;

public class UserController(IUserService service) : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
        => ApiResult(await service.Login(dto));
    
    [HttpPost("refresh-token-login/{refreshToken}")]
    public async Task<IActionResult> RefreshTokenLogin([FromRoute] string refreshToken)
        => ApiResult(await service.LoginWithRefreshToken(refreshToken));
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto user)
        => ApiResult(await service.Register(user));
}
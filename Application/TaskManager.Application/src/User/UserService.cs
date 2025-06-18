using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TaskManager.Application.Abstraction.src;
using TaskManager.Application.Abstraction.src.User.Dto;
using TaskManager.Common.Exceptions;
using TaskManager.Common.Models.Response;
using TaskManager.Common.Models.Token;
using TaskManager.Domain.Entities.Identity;
using TaskManager.Infrastructure;

namespace TaskManager.Application.src;

public class UserService(UserManager<User> service, ITokenHandler tokenHandler, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : IUserService
{
    public string? GetCurrentUsername() => httpContextAccessor.HttpContext?.User.Identity!.Name;
    public async Task<ServiceResponse<Token>> Login(LoginDto dto)
    {
        var user = await service.FindByNameAsync(dto.UsernameOrEmail) 
                   ?? await service.FindByEmailAsync(dto.UsernameOrEmail)
                   ?? throw new NotFoundException("User Not Found");
        var result = await service.CheckPasswordAsync(user, dto.Password);
        if (!result) throw new Exception();
        var token = tokenHandler.CreateToken(user);
        await UpdateRefreshTokenAsync(token.RefreshToken!, user, token.Expiration, 30);
        return ServiceResponse<Token>.Success(token, StatusCodes.Status200OK);
    }
    public async Task<ServiceResponse<Token>> LoginWithRefreshToken(string refreshToken)
    {
        var user = await service.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        if (user == null) throw new NotFoundException("User Not Found");
        var token = tokenHandler.CreateToken(user);
        await UpdateRefreshTokenAsync(refreshToken, user, token.Expiration, 10);
        return ServiceResponse<Token>.Success(token, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse> Register(RegisterDto model)
    {
        await Validations(model);
        var user = mapper.Map<User>(model);
        var result = await service.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new BadRequestException(result.Errors.Select(x => x.Description).Aggregate((x, y) => $"{x}, {y}"));
        return ServiceResponse.Success(StatusCodes.Status201Created);
    }
    private async Task UpdateRefreshTokenAsync(string refreshToken, User user, DateTime accessTokenDate, int addToAccessToken)
    {
        if(user == null) throw new NotFoundException("User Not Found");
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiration = accessTokenDate.AddMinutes(addToAccessToken);
        await service.UpdateAsync(user);
    }
    public Guid GetCurrentUserId()
    {
        var userId = httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == "Id").Value;
        if(userId == null)
            throw new NotFoundException("User not found");
        return Guid.Parse(userId);
    }

    private async Task Validations(RegisterDto user)
    {
        if(user.Password != user.ConfirmPassword)
            throw new BadRequestException("Passwords do not match");
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using TaskManager.Application.Abstraction;
using TaskManager.Application.Abstraction.Cache;
using TaskManager.Application.Abstraction.src;
using TaskManager.Application.Cache;
using TaskManager.Application.src;
using TaskManager.Application.src.Abstraction.Base;
using TaskManager.Application.src.Base;
using TaskManager.Application.src.Mapping;

namespace TaskManager.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserMapping));
        services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<ICommentService, CommentService>();
        //add services -> will use reflection to register all services
        return services;
    }
    public static IServiceCollection AddCachingServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache(); // This caching approach is suitable for single-server applications only. It will not work reliably if the application is scaled to multiple servers.

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["RedisConfiguration:Url"];
            options.InstanceName = configuration["RedisConfiguration:InstanceName"];
        });
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var config = ConfigurationOptions.Parse(configuration["RedisConfiguration:Url"], true);
            config.AbortOnConnectFail = false;
            return ConnectionMultiplexer.Connect(config);
        });
        services.AddScoped<IMemoryCacheService, MemoryCacheService>(); // This caching approach is suitable for single-server applications only. It will not work reliably if the application is scaled to multiple servers.
        services.AddScoped<IRedisCacheService, RedisCacheService>();
        return services;
    }
}
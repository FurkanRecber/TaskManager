using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.Entities.Identity;
using TaskManager.Persistence.Contexts;
using TaskManager.Persistence.Repository;
using TaskManager.Persistence.UnitOfWork;

namespace TaskManager.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskManagerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MsSQL")));
        services.AddIdentity<User, Role>().AddEntityFrameworkStores<TaskManagerDbContext>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        return services;
    }
}
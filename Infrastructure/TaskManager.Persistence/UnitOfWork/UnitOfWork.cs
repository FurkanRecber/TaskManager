using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Persistence.Contexts;

namespace TaskManager.Persistence.UnitOfWork;

public class UnitOfWork(TaskManagerDbContext context) : IUnitOfWork
{
    public void Commit()
    {
        context.SaveChanges();
    }
    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }
}
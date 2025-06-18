using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Models.Entities;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Entities.Identity;
using Task = TaskManager.Domain.Entities.Task;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace TaskManager.Persistence.Contexts;

public class TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
    : IdentityDbContext<User, Role, Guid>(options)
{
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Task)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TaskId)
            .OnDelete(DeleteBehavior.Restrict); 
        
        modelBuilder.Entity<Task>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Comment>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Tag>().HasQueryFilter(x => !x.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        ApplyAuditInfo();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInfo();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditInfo()
    {
        var entries = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            var entity = entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedDate = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entity.UpdatedDate = DateTime.UtcNow;
            }
        }
    }
}

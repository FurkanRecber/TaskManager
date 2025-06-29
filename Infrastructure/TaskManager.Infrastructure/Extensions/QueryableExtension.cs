using System.Linq.Expressions;

namespace TaskManager.Infrastructure.Extensions;

public static class QueryableExtension
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> queryable, bool condition, Expression<Func<T, bool>> predicate) 
            =>  condition ? queryable.Where(predicate) : queryable;
}
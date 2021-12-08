using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CoreRepository.Interfaces
{
    public interface IReadOnlyRepository<T, TDbContext> 
        where T : class, new()
        where TDbContext : DbContext
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate);

        DbSet<T> GetQueryable();

        bool Exists(Expression<Func<T, bool>> predicate = null);

        long Count(Expression<Func<T, bool>> predicate = null);

        TResult Max<TResult>(Expression<Func<T, TResult>> selector);
    }
}

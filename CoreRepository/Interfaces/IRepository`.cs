using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CoreRepository.Interfaces
{
    public interface IRepository<T, TDbContext> 
        where T : class, new() 
        where TDbContext : DbContext
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate);

        DbSet<T> GetQueryable();

        bool Exists(Expression<Func<T, bool>> predicate = null);

        long Count(Expression<Func<T, bool>> predicate = null);

        void Insert(T model);

        void BulkInsert(IEnumerable<T> models);

        void Update(T model, Func<T, bool> detachPredicate = null);

        void BulkUpdate(IEnumerable<T> models, Func<T, bool> detachListPredicate = null);

        void Delete(T model);

        void BulkDelete(IEnumerable<T> models);

        TResult Max<TResult>(Expression<Func<T, TResult>> selector);
    }
}

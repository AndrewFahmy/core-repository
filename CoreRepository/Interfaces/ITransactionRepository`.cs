using System.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoreRepository.Interfaces
{
    public interface ITransactionRepository<T, TDbContext> : IDisposable
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

        void SaveChanges();

        IDbContextTransaction CreateTransaction();

        TResult Max<TResult>(Expression<Func<T, TResult>> selector);
    }
}

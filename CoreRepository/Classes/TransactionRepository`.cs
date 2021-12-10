using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoreRepository
{
    public class TransactionRepository<T, TDbContext> : ITransactionRepository<T, TDbContext>
        where T : class, new()
        where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        private IDbContextTransaction _transaction => _context.Database.CurrentTransaction;

        public TransactionRepository(TDbContext context)
        {
            _context = context;
        }



        public void BulkDelete(IEnumerable<T> models)
        {
            _context.Set<T>().RemoveRange(models);
        }

        public void BulkInsert(IEnumerable<T> models)
        {
            _context.Set<T>().AddRange(models);
        }

        public void BulkUpdate(IEnumerable<T> models, Func<T, bool> detachListPredicate = null)
        {
            _context.Set<T>().UpdateRange(models);
        }

        public long Count(Expression<Func<T, bool>> predicate = null)
        {
            return _context.Set<T>().LongCount(predicate ?? (p => true));
        }

        public void Delete(T model)
        {
            _context.Set<T>().Remove(model);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }

        public bool Exists(Expression<Func<T, bool>> predicate = null)
        {
            return _context.Set<T>().Any(predicate ?? (p => true));
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public DbSet<T> GetQueryable()
        {
            return _context.Set<T>();
        }

        public void Insert(T model)
        {
            _context.Set<T>().Add(model);
        }

        public TResult Max<TResult>(Expression<Func<T, TResult>> selector)
        {
            if (_context.Set<T>().Count() <= 0) return default(TResult);

            return _context.Set<T>().Max(selector);
        }

        public IDbContextTransaction CreateTransaction()
        {
            return _transaction ?? _context.Database.BeginTransaction();
        }

        public void SaveChanges()
        {
            _transaction?.Commit();

            _context.SaveChanges();
        }

        public void Update(T model, Func<T, bool> detachPredicate = null)
        {
            _context.Set<T>().Update(model);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FrameworkRepository.Helpers;
using FrameworkRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FrameworkRepository
{
    public class Repository<T, TDbContext> : IRepository<T, TDbContext>
        where T : class, new()
        where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        public Repository(TDbContext context)
        {
            _context = context;
        }


        public void BulkDelete(IEnumerable<T> models, Func<T, bool> detachListPredicate = null)
        {
            if (detachListPredicate != null)
                _context.DetachList(detachListPredicate);

            _context.Set<T>().RemoveRange(models);

            _context.SaveChanges();
        }

        public void BulkInsert(IEnumerable<T> models)
        {
            _context.Set<T>().AddRange(models);

            _context.SaveChanges();
        }

        public void BulkUpdate(IEnumerable<T> models, Func<T, bool> detachListPredicate = null)
        {
            if (detachListPredicate != null)
                _context.DetachList(detachListPredicate);

            _context.Set<T>().UpdateRange(models);

            _context.SaveChanges();
        }

        public long Count(Expression<Func<T, bool>> predicate = null)
        {
            return _context.Set<T>().LongCount(predicate ?? (p => true));
        }

        public void Delete(T model, Func<T, bool> detachPredicate = null)
        {
            if (detachPredicate != null)
                _context.DetachLocal(detachPredicate);

            _context.Set<T>().Remove(model);

            _context.SaveChanges();
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

            _context.SaveChanges();
        }

        public TResult Max<TResult>(Expression<Func<T, TResult>> selector)
        {
            if (_context.Set<T>().Count() <= 0) return default(TResult);

            return _context.Set<T>().Max(selector);
        }

        public void Update(T model, Func<T, bool> detachPredicate = null)
        {
            if (detachPredicate != null)
                _context.DetachLocal(detachPredicate);

            _context.Set<T>().Update(model);

            _context.SaveChanges();
        }
    }
}

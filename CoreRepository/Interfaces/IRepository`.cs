using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CoreRepository.Interfaces
{
    public interface IRepository<T, TDbContext> : IReadOnlyRepository<T, TDbContext>
        where T : class, new()
        where TDbContext : DbContext
    {
        void Insert(T model);

        void BulkInsert(IEnumerable<T> models);

        void Update(T model, Func<T, bool> detachPredicate = null);

        void BulkUpdate(IEnumerable<T> models, Func<T, bool> detachListPredicate = null);

        void Delete(T model);

        void BulkDelete(IEnumerable<T> models);
    }
}

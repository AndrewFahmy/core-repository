using System.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FrameworkRepository.Interfaces
{
    public interface ITransactionRepository<T, TDbContext> : IRepository<T, TDbContext>, IDisposable
        where T : class, new() 
        where TDbContext : DbContext
    {
        void SaveChanges();

        IDbContextTransaction CreateTransaction();
    }
}

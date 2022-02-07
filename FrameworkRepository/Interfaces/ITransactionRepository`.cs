using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FrameworkRepository.Interfaces
{
    public interface ITransactionRepository<T, TDbContext> : IRepository<T, TDbContext>
        where T : class, new()
        where TDbContext : DbContext
    {
        void SaveChanges();

        IDbContextTransaction CreateTransaction();
    }
}

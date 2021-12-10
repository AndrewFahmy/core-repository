using FrameworkRepository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FrameworkRepository
{
    public static class RegisterRepositories
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped(typeof(IRawRepository<>), typeof(RawRepository<>));
            services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
            services.AddScoped(typeof(ITransactionRepository<,>), typeof(TransactionRepository<,>));
        }
    }
}

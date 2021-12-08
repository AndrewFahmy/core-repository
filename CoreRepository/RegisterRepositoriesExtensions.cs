using CoreRepository.Classes;
using CoreRepository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CoreRepository
{
    public static class RegisterRepositoriesExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped(typeof(IRawRepository<>), typeof(RawRepository<>));
            services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
            services.AddScoped(typeof(ITransactionRepository<,>), typeof(TransactionRepository<,>));
        }
    }
}

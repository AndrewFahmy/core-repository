using FrameworkRepository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FrameworkRepository
{
    public static class RegisterRepositories
    {
        public static void AddRepositories(this IServiceCollection services, bool isConsole = false)
        {
            if (isConsole)
            {
                services.AddSingleton(typeof(IRepository<,>), typeof(Repository<,>));
                services.AddSingleton(typeof(IRawRepository<>), typeof(RawRepository<>));
                services.AddSingleton(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
                services.AddSingleton(typeof(ITransactionRepository<,>), typeof(TransactionRepository<,>));
            }
            else
            {
                services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
                services.AddScoped(typeof(IRawRepository<>), typeof(RawRepository<>));
                services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
                services.AddScoped(typeof(ITransactionRepository<,>), typeof(TransactionRepository<,>));
            }
        }
    }
}

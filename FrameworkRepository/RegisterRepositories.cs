using FrameworkRepository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FrameworkRepository
{
    public static class RegisterRepositories
    {
        public static void AddRepositories(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(typeof(IRepository<,>), typeof(Repository<,>));
                    services.AddSingleton(typeof(IRawRepository<>), typeof(RawRepository<>));
                    services.AddSingleton(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
                    services.AddSingleton(typeof(ITransactionRepository<,>), typeof(TransactionRepository<,>));
                    break;

                case ServiceLifetime.Transient:
                    services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
                    services.AddTransient(typeof(IRawRepository<>), typeof(RawRepository<>));
                    services.AddTransient(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
                    services.AddTransient(typeof(ITransactionRepository<,>), typeof(TransactionRepository<,>));
                    break;


                default:
                    services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
                    services.AddScoped(typeof(IRawRepository<>), typeof(RawRepository<>));
                    services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
                    services.AddScoped(typeof(ITransactionRepository<,>), typeof(TransactionRepository<,>));
                    break;
            }

        }
    }
}

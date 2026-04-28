using BobsCorn.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace BobsCorn.Infrastructure.Configurations
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDbConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
            
            return services;
        }
    }
}

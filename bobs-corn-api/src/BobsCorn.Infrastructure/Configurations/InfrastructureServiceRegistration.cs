using BobsCorn.Application.Clock;
using BobsCorn.Application.Corn.BuyCorn;
using BobsCorn.Application.RateLimiting;
using BobsCorn.Infrastructure.Common;
using BobsCorn.Infrastructure.Persistence;
using BobsCorn.Infrastructure.Persistence.Dapper;
using BobsCorn.Infrastructure.RateLimiting;
using Microsoft.Extensions.DependencyInjection;

namespace BobsCorn.Infrastructure.Configurations
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // Database
            services.AddSingleton<IDbConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

            services.AddSingleton<IClock, SystemClock>();
            services.AddSingleton<IRateLimiter, InMemoryCornRateLimiter>();

            services.AddScoped<ICornPurchaseStore, CornPurchaseStore>();

            return services;
        }
    }
}

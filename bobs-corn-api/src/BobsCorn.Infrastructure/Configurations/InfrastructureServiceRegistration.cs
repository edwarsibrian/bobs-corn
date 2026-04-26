using Microsoft.Extensions.DependencyInjection;

namespace BobsCorn.Infrastructure.Configurations
{
    public static class InfrastructureServiceRegistration
    {
            public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            {
                return services;
        }
    }
}

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BobsCorn.Application.Configurations
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Fluent Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

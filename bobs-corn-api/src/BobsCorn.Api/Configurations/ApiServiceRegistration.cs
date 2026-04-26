using BobsCorn.Api.Settings;
using BobsCorn.Application.Configurations;
using BobsCorn.Infrastructure.Configurations;

namespace BobsCorn.Api.Configurations
{
    public static class ApiServiceRegistration
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            //Settings
            services.Configure<CorsSettings>(configuration.GetSection("Cors"));

            var corsSettings = configuration
                .GetSection("Cors")
                .Get<CorsSettings>() ?? throw new InvalidOperationException("Cors settings are not configured properly.");

            //Add CORS policy
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(corsSettings.AllowedOrigins)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .WithExposedHeaders("Retry-After");
                });
            });


            //Application
            services.AddApplication();

            //Infrastructure
            services.AddInfrastructure();

            return services;
        }
    }
}

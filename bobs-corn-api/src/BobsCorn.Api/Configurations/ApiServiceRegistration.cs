using BobsCorn.Api.Exceptions;
using BobsCorn.Api.Settings;
using BobsCorn.Api.Swagger;
using BobsCorn.Application.Configurations;
using BobsCorn.Infrastructure.Configurations;

namespace BobsCorn.Api.Configurations
{
    public static class ApiServiceRegistration
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            //Settings
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

            //Swagger
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<AddClientIdHeaderOperationFilter>();
            });

            //Exception handling and details
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            //Application
            services.AddApplication();

            //Infrastructure
            var connectionString = configuration.GetConnectionString("BobsCorn") 
                ?? throw new InvalidOperationException("Connection string 'BobsCorn' is not configured.");
            
            services.AddInfrastructure(connectionString);


            return services;
        }
    }
}

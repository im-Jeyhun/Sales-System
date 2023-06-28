using AspNetCoreRateLimit;
using SalesSystem.Application.Interfaces;
using SalesSystem.Application.Services;
using SalesSystem.Core.Entities;
using SalesSystem.Services.Concretes;

namespace SalesSystem.Api.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IDiscountRepository, DiscountRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
        }
    }
}

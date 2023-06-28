using SalesSystem.Application.Context;

namespace SalesSystem.Api.Infrastructure.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton(new SqlServerConnection(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}

using SalesSystem.Api.Infrastructure.Extensions;

namespace SalesSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureServices(builder.Configuration);

            var app = builder.Build();

            
            app.ConfigureMiddlewarePipeline();

            app.Run();
        }
    }
}
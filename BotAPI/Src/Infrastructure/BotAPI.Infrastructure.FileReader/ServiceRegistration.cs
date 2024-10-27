using BotAPI.Infrastructure.FileReader.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotAPI.Infrastructure.FileReader
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddFileReaderInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<AnyFileReaderFactory>();
            return services;
        }
    }
}

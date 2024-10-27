using Microsoft.Extensions.DependencyInjection;
using BotAPI.Application.Interfaces;
using BotAPI.Infrastructure.Resources.Services;

namespace BotAPI.Infrastructure.Resources
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddResourcesInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ITranslator, Translator>();

            return services;
        }
    }
}

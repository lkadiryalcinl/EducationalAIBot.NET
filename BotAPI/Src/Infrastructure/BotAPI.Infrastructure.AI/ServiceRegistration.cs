using BotAPI.Application.Interfaces;
using BotAPI.Infrastructure.AI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotAPI.Infrastructure.AI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddAIInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAIProcessService, AIProcessService>();

            return services;
        }
    }
}

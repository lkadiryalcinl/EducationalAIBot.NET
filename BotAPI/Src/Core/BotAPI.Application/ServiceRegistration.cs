using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BotAPI.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

            return services;
        }
    }
}

using Application.Interfaces;
using Application.Services;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions
{
    public static class InjectionExtensions
    {
        [Obsolete]
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddSingleton(configuration);

            _ = services.AddFluentValidation(option =>
            {
                _ = option.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            });

            _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());

            _ = services.AddScoped<IUserApplication, UserApplication>();

            return services;
        }
    }
}

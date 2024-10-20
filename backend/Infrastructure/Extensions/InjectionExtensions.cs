using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Interfaces;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string? assembly = typeof(DatabaseContext).Assembly.FullName;

            _ = services.AddDbContext<DatabaseContext>(
                option => option.UseInMemoryDatabase(databaseName: "InMemory_DB")
            );

            _ = services.AddTransient<IUnitOfWork, UnitOfWork>();
            _ = services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}

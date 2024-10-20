using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Utilities.Static;

namespace Infrastructure.Persistence.Contexts.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using DatabaseContext _context = new(serviceProvider.
                GetRequiredService<DbContextOptions<DatabaseContext>>());
            if (_context.Users.Any())
            {
                return;
            }

            _context.Users.AddRange(
                new User
                {
                    Id = 1,
                    Name = "Administrator",
                    LastName = "",
                    Email = "admin@admin.com",
                    UserName = "Admin",
                    Password = "Admin1234**",
                    Image = "",
                    AuditCreateUser = 0,
                    AuditCreateDate = DateTime.UtcNow,
                    AuditUpdateUser = null,
                    AuditUpdateDate = null,
                    AuditDeleteUser = null,
                    AuditDeleteDate = null,
                    State = Convert.ToBoolean(StateTypes.Active)
                }
            );

            _ = _context.SaveChanges();
        }
    }
}

using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<User> AccountByUserName(string userName)
        {
            User? account = await _databaseContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName!.Equals(userName));

            return account!;
        }
    }
}

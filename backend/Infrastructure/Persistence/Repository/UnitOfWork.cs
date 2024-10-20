using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Interfaces;

namespace Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;
        public IUserRepository User { get; private set; }

        public UnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            User = new UserRepository(_databaseContext);
        }

        public void Dispose()
        {
            _databaseContext.Dispose();
        }

        public void SaveChanges()
        {
            _ = _databaseContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            _ = await _databaseContext.SaveChangesAsync();
        }
    }
}

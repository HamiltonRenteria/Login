using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Interfaces;

namespace Infrastructure.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DatabaseContext _databaseContext;

        public GenericRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> RegisterAsync(T entity)
        {
            entity.AuditCreateUser = 1;
            entity.AuditCreateDate = DateTime.Now;

            _ = await _databaseContext.AddAsync(entity);

            int recordAffected = await _databaseContext.SaveChangesAsync();
            return recordAffected > 0;
        }
    }
}
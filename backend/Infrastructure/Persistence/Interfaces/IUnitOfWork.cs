namespace Infrastructure.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}

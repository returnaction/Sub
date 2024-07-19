using Sub.Repository.BaseRepository;

namespace Sub.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        void Commit();
        Task CommitAsync();
        IGenericRepository<T> GetGenericRepository<T>() where T : class, new();

    }
}

using System.Linq.Expressions;

namespace Sub.Repository.BaseRepository
{
    public interface IGenericRepository<T> where T : class , new()
    {
        Task<T> GetEnityByIdAsync(Guid id);
        IQueryable GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);


    }
}

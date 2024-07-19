using System.Linq.Expressions;

namespace Sub.Repository.BaseRepository
{
    public interface IGenericRepository<T> where T : class , new()
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetEnityByIdAsync(Guid id);
        IQueryable GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);


    }
}

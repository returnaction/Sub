using System.Linq.Expressions;

namespace Sub.Repository.BaseRepository
{
    public interface IGenericRepository<T> where T : class , new()
    {
        Task<T> GetEnityByIdAsync(int id);
        IQueryable GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties);

        Task AddAsync(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);


    }
}

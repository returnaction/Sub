using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Sub.Repository.BaseRepository
{
    public interface IGenericRepository<T> where T : class , new()
    {
        Task<T> GetEnityByIdAsync(int id);
        IQueryable GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IIncludableQueryable<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath);
        IIncludableQueryable<T, TProperty> ThenInclude<TPreviousProperty, TProperty>(IIncludableQueryable<T, TPreviousProperty> query, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath);

        Task AddAsync(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);


    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sub.Data;
using System.Linq.Expressions;

namespace Sub.Repository.BaseRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void DeleteEntity(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable GetAllAsync()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetEnityByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void UpdateEntity(T entity)
        {
            _dbSet.Update(entity);
        }

        public  IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return  _dbSet.Where(predicate);
        }

        public IIncludableQueryable<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath)
        {
            return _dbSet.Include(navigationPropertyPath);
        }

        public IIncludableQueryable<T, TProperty> ThenInclude<TPreviousProperty, TProperty>(IIncludableQueryable<T, TPreviousProperty> query, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
        {
            return query.ThenInclude(navigationPropertyPath);
        }
    }
}


using Microsoft.EntityFrameworkCore;
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

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
    }
}

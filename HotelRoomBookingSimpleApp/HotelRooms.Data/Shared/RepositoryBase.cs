using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelRooms.Persistence.Shared
{
    public class RepositoryBase<T, C, K> : IRepository<T, K>
        where T : class
        where C : DbContext
    {
        protected readonly DbSet<T> _set;
        protected readonly DbContext _dbContext;

        protected DbSet<T> Query
        {
            get { return _set; }
            set { }
        }

        public RepositoryBase(C dbContext)
        {
            _dbContext = dbContext;
            _set = dbContext.Set<T>();
        }

        public ValueTask<T> FindAsync(K id)
        {
            return _set.FindAsync(id);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return _set.FirstOrDefaultAsync(predicate);
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            var query = _set.AsNoTracking();

            if(predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public IQueryable<T> GetQuerable(Expression<Func<T, bool>> predicate)
        {
            return _set.AsNoTracking().Where(predicate);
        }
        public async Task<IList<T>> GetAllTrackedAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.Where(predicate).ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _set.CountAsync();
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return _set.CountAsync(predicate);
        }

        public void Add(T entityToAdd)
        {
            _set.Add(entityToAdd);
        }

        public void Update(T entityToUpdate)
        {
            _set.Update(entityToUpdate);
        }

        public void Delete(T entityToDelete)
        {
            _set.Remove(entityToDelete);
        }
    }
}

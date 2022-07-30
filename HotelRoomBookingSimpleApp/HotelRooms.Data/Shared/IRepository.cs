using System.Linq.Expressions;

namespace HotelRooms.Persistence.Shared
{
    public interface IRepository<TEntity, K> where TEntity : class
    {
        ValueTask<TEntity> FindAsync(K id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<IList<TEntity>> GetAllTrackedAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entityToAdd);
        void Update(TEntity entityToUpdate);
        void Delete(TEntity entityToDelete);
        IQueryable<TEntity> GetQuerable(Expression<Func<TEntity, bool>> predicate);
    }
}

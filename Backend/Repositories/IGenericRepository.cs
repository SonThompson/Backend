using System.Linq.Expressions;

namespace WebDb.Repositories
{
    public interface IGenericRepository<IEntity> where IEntity : class
    {
        Task<IEnumerable<IEntity>> All();
        Task<IEntity> GetById(Guid id);
        Task<bool> Add(IEntity entity);
        Task<bool> Delete(Guid id);
        Task<bool> Upsert(IEntity entity);
        Task<IEnumerable<IEntity>> Find(Expression<Func<IEntity, bool>> predicate);

        Task<bool> SaveAsync();
    }
}

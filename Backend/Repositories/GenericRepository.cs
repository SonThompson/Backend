using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using WebDb.Helpers;

namespace WebDb.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        // Переменные класса объявляются для контекста базы данных и для набора сущностей
        // для которого создаётся репозиторий
        protected internal ContextDb context;
        protected internal DbSet<TEntity> dbSet;
        public readonly ILogger logger;

        // Конструктор принимает экземпляр контекста базы данных и инициализирует переменную набора сущностей:
        public GenericRepository(ContextDb context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
            this.logger = logger;
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<TEntity>> All()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.Where(predicate).AsNoTracking().ToListAsync();
        }

        public virtual Task<bool> Upsert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAsync()
        {
            await context.SaveChangesAsync();
            return true;
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq.Expressions;

namespace Backend.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Переменные класса объявляются для контекста базы данных, логгера и набора сущностей
        /// для которого создаётся репозиторий
        /// </summary>
        protected internal ContextDb context;
        protected internal DbSet<TEntity> dbSet;
        public readonly ILogger logger;
        public readonly IMapper mapper;

        /// <summary>
        /// Конструктор принимает экземпляр контекста базы данных, логгера и инициализирует переменную набора сущностей:
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public GenericRepository(ContextDb context, ILogger logger, IMapper mapper)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
            this.logger = logger;
            this.mapper = mapper;
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

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            await context.SaveChangesAsync();
            return true;
        }
    }
}

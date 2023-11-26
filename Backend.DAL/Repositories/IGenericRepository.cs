using System.Linq.Expressions;

namespace Backend.DAL.Repositories
{
    public interface IGenericRepository<IEntity> where IEntity : class
    {
        /// <summary>
        /// Получение Сущности по идентификатору
        /// </summary>
        /// <param name="id">идентификатор</param>
        Task<IEntity> GetById(Guid id);

        /// <summary>
        /// Добавление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        Task<bool> Add(IEntity entity);

        /// <summary>
        /// Нахождение сущности по предикату
        /// </summary>
        /// <param name="predicate">предикат</param>
        Task<IEnumerable<IEntity>> Find(Expression<Func<IEntity, bool>> predicate);

        /// <summary>
        /// Сохранение изменений
        /// </summary>
        Task<bool> SaveAsync();
    }
}

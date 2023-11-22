using BackendEntities.Entities;

namespace BackendHelper.Repositories.IRepos
{
    public interface ICategoryRepo : IGenericRepository<Category>
    {
        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="id">идентификатор</param>
        public Task<bool> Delete(Guid id);
        /// <summary>
        /// Получение всех категорий
        /// </summary>
        /// <returns>result</returns>
        public Task<IEnumerable<Category>> All();

        /// <summary>
        /// Обновление категории
        /// </summary>
        /// <param name="entity">параметры категории</param>
        /// <returns>result</returns>
        public Task<bool> Update(Category entity);

    }
}

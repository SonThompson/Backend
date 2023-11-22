using BackendEntities.Entities;

namespace BackendHelper.Repositories.IRepos
{
    public interface IProductRepo : IGenericRepository<Product>
    {
        /// <summary>
        /// Удаление продукта
        /// </summary>
        /// <param name="id">идентификатор</param>
        public Task<bool> Delete(Guid id);
        /// <summary>
        /// Получение всех продуктов
        /// </summary>
        /// <returns>result</returns>
        public Task<IEnumerable<Product>> All();
        /// <summary>
        /// Обновление продукта
        /// </summary>
        /// <param name="entity">параметры продукта</param>
        /// <returns>result</returns>
        public Task<bool> Update(Product entity);
    }
}

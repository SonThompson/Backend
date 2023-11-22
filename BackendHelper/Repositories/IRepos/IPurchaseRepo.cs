using BackendEntities.Entities;

namespace BackendHelper.Repositories.IRepos
{
    public interface IPurchaseRepo : IGenericRepository<Purchase>
    {
        /// <summary>
        /// Удаление покупки
        /// </summary>
        /// <param name="id">идентификатор</param>
        public Task<bool> Delete(Guid id);
        /// <summary>
        /// Получение всех покупок
        /// </summary>
        /// <returns>result</returns>
        public Task<IEnumerable<Purchase>> All();
        /// <summary>
        /// Обновление покупок
        /// </summary>
        /// <param name="entity">параметры покупок</param>
        /// <returns>result</returns>
        public Task<bool> Update(Purchase entity);
    }
}

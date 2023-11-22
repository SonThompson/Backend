using BackendEntities.Entities;

namespace BackendHelper.Repositories.IRepos
{
    public interface IStoreRepo : IGenericRepository<Store>
    {
        /// <summary>
        /// Удаление филиала
        /// </summary>
        /// <param name="id">идентификатор</param>
        public Task<bool> Delete(Guid id);
        /// <summary>
        /// Получение всех филиалов
        /// </summary>
        /// <returns>result</returns>
        public Task<IEnumerable<Store>> All();
        /// <summary>
        /// Обновление филиала
        /// </summary>
        /// <param name="entity">параметры филиала</param>
        /// <returns>result</returns>
        public Task<bool> Update(Store entity);
    }
}

using BackendEntities.Entities;

namespace BackendHelper.Repositories.IRepos
{
    public interface ICustomerRepo : IGenericRepository<Customer>
    {
        /// <summary>
        /// Удаление Заказчика
        /// </summary>
        /// <param name="id">идентификатор</param>
        public Task<bool> Delete(Guid id);
        /// <summary>
        /// Получение всех заказчиков
        /// </summary>
        /// <returns>result</returns>
        public Task<IEnumerable<Customer>> All();
        /// <summary>
        /// Обновление заказчика
        /// </summary>
        /// <param name="entity">параметры заказчика</param>
        /// <returns>result</returns>
        public Task<bool> Update(Customer entity);
    }
}

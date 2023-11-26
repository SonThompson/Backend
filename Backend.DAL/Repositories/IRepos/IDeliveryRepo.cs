using Backend.DAL.Entities;
using Backend.DAL.Repositories;

namespace BackendHelper.Repositories.IRepos
{
    public interface IDeliveryRepo : IGenericRepository<Delivery>
    {
        /// <summary>
        /// Получение всех доставок с возможностью сортировки
        /// </summary>
        /// <param name="SortByCount">Сортировать по числу доставок</param>
        /// <param name="SortByDate">Сортировать по дате доставки</param>
        /// <returns>result</returns>
        Task<IEnumerable<Delivery>> AllWithSort(bool SortByCount, bool SortByDate);
        /// <summary>
        /// Удаление доставки
        /// </summary>
        /// <param name="id">идентификатор</param>
        public Task<bool> Delete(Guid id);
        /// <summary>
        /// Обновление доставки
        /// </summary>
        /// <param name="entity">параметры доставки</param>
        /// <returns>result</returns>
        public Task<bool> Update(Delivery entity);
    }
}

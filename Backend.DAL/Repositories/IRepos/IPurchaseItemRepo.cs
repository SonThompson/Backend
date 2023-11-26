using Backend.DAL.Entities;
using Backend.DAL.Repositories;

namespace BackendHelper.Repositories.IRepos
{
    public interface IPurchaseItemRepo : IGenericRepository<PurchaseItem>
    {
        /// <summary>
        /// Получение всей информации о товарах с возсожностью сортировки
        /// </summary>
        /// <param name="SortByPrice">Сортировать по цене</param>
        /// <param name="SortByCount">Сортировать по количеству товара</param>
        /// <returns></returns>
        Task<IEnumerable<PurchaseItem>> AllWithSort(bool SortByPrice, bool SortByCount);
        /// <summary>
        /// Удаление информации о товаре
        /// </summary>
        /// <param name="id">идентификатор</param>
        public Task<bool> Delete(Guid id);
        /// <summary>
        /// Обновление информации о товаре
        /// </summary>
        /// <param name="entity">параметры информации о товаре</param>
        /// <returns>result</returns>
        public Task<bool> Update(PurchaseItem entity);
    }
}

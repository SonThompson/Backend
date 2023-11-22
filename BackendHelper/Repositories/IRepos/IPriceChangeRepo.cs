using BackendEntities.Entities;

namespace BackendHelper.Repositories.IRepos
{
    public interface IPriceChangeRepo : IGenericRepository<PriceChange>
    {
        /// <summary>
        /// Получение всех изменений цен с возможностью сортировки
        /// </summary>
        /// <param name="SortByDate">Сортировка по дате изменения цены</param>
        /// <param name="SortByPrice">Сортировка по цене</param>
        /// <returns>result</returns>
        Task<IEnumerable<PriceChange>> AllWithSort(bool SortByDate, bool SortByPrice);
        /// <summary>
        /// Удаление изменения цены
        /// </summary>
        /// <param name="id">идентификатор</param>
        public Task<bool> Delete(Guid id);
        /// <summary>
        /// Обновление изменения цены
        /// </summary>
        /// <param name="entity">параметры изменения цены</param>
        /// <returns>result</returns>
        public Task<bool> Update(PriceChange entity);
    }
}

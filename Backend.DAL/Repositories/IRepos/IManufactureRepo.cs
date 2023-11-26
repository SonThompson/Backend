using Backend.DAL.Entities;
using Backend.DAL.Repositories;

namespace BackendHelper.Repositories.IRepos
{
    public interface IManufactureRepo : IGenericRepository<Manufacture>
    {
        /// <summary>
        /// Удаление мануфактуры
        /// </summary>
        /// <param name="id">идентификатор</param>
        public Task<bool> Delete(Guid id);
        /// <summary>
        /// Получение всех мануфактур
        /// </summary>
        /// <returns>result</returns>
        public Task<IEnumerable<Manufacture>> All();
        /// <summary>
        /// Обновление мануфактуры
        /// </summary>
        /// <param name="entity">параметры мануфактуры</param>
        /// <returns>result</returns>
        public Task<bool> Update(Manufacture entity);
    }
}

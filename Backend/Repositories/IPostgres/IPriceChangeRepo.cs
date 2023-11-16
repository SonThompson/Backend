using WebDb.Entities;

namespace WebDb.Repositories.IPostgres
{
    public interface IPriceChangeRepo : IGenericRepository<PriceChange>
    {
        Task<IEnumerable<PriceChange>> AllWithSort(bool SortByDate, bool SortByPrice);
    }
}

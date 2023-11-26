using Backend.DAL.Entities;
using BackendHelper.Repositories.IRepos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Backend.DAL.Repositories.repos
{
    public class PriceChangeRepository : GenericRepository<PriceChange>, IPriceChangeRepo
    {
        public PriceChangeRepository(ContextDb context, ILogger logger, IMapper mapper) : base(context, logger, mapper) { }

        public async Task<IEnumerable<PriceChange>> AllWithSort(bool SortByDate, bool SortByPrice)
        {
                if (SortByDate)
                {
                    return await dbSet.OrderBy(s => s.DataPriceChange).AsNoTracking().ToListAsync();
                }
                else if (SortByPrice)
                {
                    return await dbSet.OrderBy(s => s.NewPrice).AsNoTracking().ToListAsync();
                }
                return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
                var entityToDelete = await dbSet.FindAsync(id);

                dbSet.Remove(entityToDelete);

                return true;
        }

        public async Task<bool> Update(PriceChange priceChange)
        {
                var entityToUpsert = await dbSet.FindAsync(priceChange.Id);

                entityToUpsert.DataPriceChange = DateTime.Now;
                entityToUpsert.NewPrice = priceChange.NewPrice;
                entityToUpsert.ProductId = priceChange.ProductId;

                return true;
        }
    }
}

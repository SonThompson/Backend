using WebDb.Entities;
using WebDb.Repositories.IPostgres;
using Microsoft.EntityFrameworkCore;
using WebDb.Helpers;

namespace WebDb.Repositories.postgres
{
    public class PriceChangeRepository : GenericRepository<PriceChange>, IPriceChangeRepo
    {
        public PriceChangeRepository(ContextDb context, ILogger logger) : base(context, logger) { }

        public async Task<IEnumerable<PriceChange>> AllWithSort(bool SortByDate, bool SortByPrice)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} All function error", typeof(PriceChangeRepository));
                return new List<PriceChange>();
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var entityToDelete = await dbSet.Where(x => x.ProductId == id).FirstOrDefaultAsync();

                if (entityToDelete == null) { return false; }

                dbSet.Remove(entityToDelete);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} Delete function error", typeof(ManufactureRepository));
                return false;
            }
        }

        public override async Task<bool> Upsert(PriceChange entity)
        {
            try
            {
                var entityToUpsert = await dbSet.Where(x => x.ProductId == entity.ProductId).FirstOrDefaultAsync();

                if (entityToUpsert == null) { return await Add(entity); }

                entityToUpsert.DataPriceChange = DateTime.Now;
                entityToUpsert.NewPrice = entity.NewPrice;
                entityToUpsert.ProductId = entity.ProductId;

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} Upsert function error", typeof(PriceChangeRepository));
                return false;
            }
        }
    }
}

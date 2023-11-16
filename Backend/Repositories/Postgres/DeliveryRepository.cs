using WebDb.Entities;
using WebDb.Repositories.IPostgres;
using Microsoft.EntityFrameworkCore;
using WebDb.Helpers;

namespace WebDb.Repositories.postgres
{
    public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepo
    {
        public DeliveryRepository(ContextDb context, ILogger logger) : base(context, logger) { }

        public async Task<IEnumerable<Delivery>> AllWithSort(bool SortByCount, bool SortByDate)
        {
            try
            {
                if (SortByCount)
                {
                    return await dbSet.OrderBy(s => s.ProductCount).AsNoTracking().ToListAsync();
                }
                else if (SortByDate)
                {
                    return await dbSet.OrderBy(s => s.DeliveryDate).AsNoTracking().ToListAsync();
                }
                return await dbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} All function error", typeof(DeliveryRepository));
                return new List<Delivery>();
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
                logger.LogError(ex, "{Repo} Delete function error", typeof(DeliveryRepository));
                return false;
            }
        }

        public override async Task<bool> Upsert(Delivery entity)
        {
            try
            {
                var entityToUpsert = await dbSet.Where(x => x.ProductId == entity.ProductId).FirstOrDefaultAsync();

                if (entityToUpsert == null) { return await Add(entity); }

                entityToUpsert.DeliveryDate = entity.DeliveryDate;
                entityToUpsert.ProductCount= entity.ProductCount;
                entityToUpsert.ProductId = entity.ProductId;
                entityToUpsert.StoreId = entity.StoreId;

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

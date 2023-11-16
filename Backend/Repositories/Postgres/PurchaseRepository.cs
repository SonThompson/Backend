using WebDb.Entities;
using WebDb.Repositories.IPostgres;
using Microsoft.EntityFrameworkCore;
using WebDb.Helpers;

namespace WebDb.Repositories.postgres
{
    public class PurchaseRepository : GenericRepository<Purchase>, IPurchaseRepo
    {
        public PurchaseRepository(ContextDb context, ILogger logger) : base(context, logger) { }

        public async Task<IEnumerable<Purchase>> All()
        {
            try
            {
                return await dbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} All function error", typeof(PurchaseRepository));
                return new List<Purchase>();
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var entitieToDelete = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (entitieToDelete == null) { return false; }
                dbSet.Remove(entitieToDelete);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} Delete function error", typeof(PurchaseRepository));
                return false;
            }
        }

        public override async Task<bool> Upsert(Purchase entity)
        {
            try
            {
                var entityToUpsert = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (entityToUpsert == null) { return await Add(entity); }

                entityToUpsert.PurshaseDate = DateTime.Now;
                entityToUpsert.StoreId = entity.StoreId;
                entityToUpsert.CustomerId = entity.CustomerId;

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} Upsert function error", typeof(PurchaseRepository));
                return false;
            }
        }
    }
}

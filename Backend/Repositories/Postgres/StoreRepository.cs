using WebDb.Entities;
using WebDb.Repositories.IPostgres;
using Microsoft.EntityFrameworkCore;
using WebDb.Helpers;

namespace WebDb.Repositories.postgres
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepo
    {
        public StoreRepository(ContextDb context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Store>> All()
        {
            try
            {
                return await dbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} All function error", typeof(StoreRepository));
                return new List<Store>();
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
                logger.LogError(ex, "{Repo} Delete function error", typeof(StoreRepository));
                return false;
            }
        }

        public override async Task<bool> Upsert(Store entity)
        {
            try
            {
                var entityToUpsert = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (entityToUpsert == null) { return await Add(entity); }

                entityToUpsert.Name= entity.Name;

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} Upsert function error", typeof(StoreRepository));
                return false;
            }
        }
    }
}

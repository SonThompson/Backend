using WebDb.Entities;
using WebDb.Repositories.IPostgres;
using Microsoft.EntityFrameworkCore;
using WebDb.Helpers;
using System.IO.Pipes;

namespace WebDb.Repositories.postgres
{
    public class PurchaseItemRepository : GenericRepository<PurchaseItem>, IPurchaseItemRepo
    {
        public PurchaseItemRepository(ContextDb context, ILogger logger) : base(context, logger) { }

        public async Task<IEnumerable<PurchaseItem>> AllWithSort(bool SortByPrice, bool SortByCount)
        {
            try
            {
                if (SortByPrice)
                {
                    return await dbSet.OrderBy(s => s.ProductPrice).AsNoTracking().ToListAsync();
                } 
                else if (SortByCount)
                {
                    return await dbSet.OrderBy(s => s.ProductCount).AsNoTracking().ToListAsync();
                }

                return await dbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} All function error", typeof(PurchaseItemRepository));
                return new List<PurchaseItem>();
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var entitieToDelete = await dbSet.Where(x => x.PurchaseId == id).FirstOrDefaultAsync();

                if (entitieToDelete == null) { return false; }
                dbSet.Remove(entitieToDelete);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} Delete function error", typeof(PurchaseItemRepository));
                return false;
            }
        }

        public override async Task<bool> Upsert(PurchaseItem entity)
        {
            try
            {
                var entityToUpsert = await dbSet.Where(x => x.PurchaseId == entity.PurchaseId).FirstOrDefaultAsync();

                if (entityToUpsert == null) { return await Add(entity); }

                entityToUpsert.ProductPrice = entity.ProductPrice;
                entityToUpsert.ProductCount = entity.ProductCount;
                entityToUpsert.ProductId = entity.ProductId;
                entityToUpsert.PurchaseId = entity.PurchaseId;

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} Upsert function error", typeof(PurchaseItemRepository));
                return false;
            }
        }

        //public override async Task<bool> Add(PurchaseItem entity)
        //{
        //    var ent = dbSet.Include(p => p.Product)
        //        .ThenInclude(pc => pc.PriceChanges)
        //        .Where(pc => pc.ProductId == entity.ProductId);

        //    await dbSet.AddAsync(entity);
        //    return true;
        //}
    }
}

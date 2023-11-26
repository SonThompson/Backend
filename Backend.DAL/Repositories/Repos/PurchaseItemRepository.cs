using Backend.DAL.Entities;
using BackendHelper.Repositories.IRepos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Backend.DAL.Repositories.repos
{
    public class PurchaseItemRepository : GenericRepository<PurchaseItem>, IPurchaseItemRepo
    {
        public PurchaseItemRepository(ContextDb context, ILogger logger, IMapper mapper) : base(context, logger, mapper) { }

        public async Task<IEnumerable<PurchaseItem>> AllWithSort(bool SortByPrice, bool SortByCount)
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

        public async Task<bool> Delete(Guid id)
        {
                var entitieToDelete = await dbSet.FindAsync(id);

                dbSet.Remove(entitieToDelete);
                return true;
        }

        public async Task<bool> Update(PurchaseItem purchaseItem)
        {
                var entityToUpsert = await dbSet.FindAsync(purchaseItem.Id);

                entityToUpsert.ProductPrice = purchaseItem.ProductPrice;
                entityToUpsert.ProductCount = purchaseItem.ProductCount;
                entityToUpsert.ProductId = purchaseItem.ProductId;
                entityToUpsert.PurchaseId = purchaseItem.PurchaseId;

                return true;
        }
    }
}

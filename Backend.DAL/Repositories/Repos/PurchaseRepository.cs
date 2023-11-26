using Backend.DAL.Entities;
using BackendHelper.Repositories.IRepos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Backend.DAL.Repositories.repos
{
    public class PurchaseRepository : GenericRepository<Purchase>, IPurchaseRepo
    {
        public PurchaseRepository(ContextDb context, ILogger logger, IMapper mapper) : base(context, logger, mapper) { }

        public async Task<IEnumerable<Purchase>> All()
        {
                return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
                var entitieToDelete = await dbSet.FindAsync(id);

                dbSet.Remove(entitieToDelete);
                return true;
        }

        public async Task<bool> Update(Purchase purchase)
        {
                var entityToUpsert = await dbSet.FindAsync(purchase.Id);

                entityToUpsert.PurshaseDate = DateTime.Now;
                entityToUpsert.StoreId = purchase.StoreId;
                entityToUpsert.CustomerId = purchase.CustomerId;

                return true;
        }
    }
}

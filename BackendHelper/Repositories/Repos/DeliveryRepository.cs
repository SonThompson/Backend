using BackendEntities.Entities;
using BackendHelper.Repositories.IRepos;
using Microsoft.EntityFrameworkCore;
using BackendEntities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace BackendHelper.Repositories.repos
{
    public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepo
    {
        public DeliveryRepository(ContextDb context, ILogger logger, IMapper mapper) : base(context, logger, mapper) { }

        public async Task<IEnumerable<Delivery>> AllWithSort(bool SortByCount, bool SortByDate)
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

        public async Task<bool> Delete(Guid id)
        {
                var entityToDelete = await dbSet.FindAsync(id);

                dbSet.Remove(entityToDelete);
                return true;
        }

        public async Task<bool> Update(Delivery delivery)
        {
                var entityToUpsert = await dbSet.FindAsync(delivery.Id);

                entityToUpsert.DeliveryDate = delivery.DeliveryDate;
                entityToUpsert.ProductCount= delivery.ProductCount;
                entityToUpsert.ProductId = delivery.ProductId;
                entityToUpsert.StoreId = delivery.StoreId;

                return true;
        }
    }
}

using BackendEntities.Entities;
using BackendHelper.Repositories.IRepos;
using Microsoft.EntityFrameworkCore;
using BackendEntities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace BackendHelper.Repositories.repos
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepo
    {
        public StoreRepository(ContextDb context, ILogger logger, IMapper mapper) : base(context, logger, mapper) { }

        public async Task<IEnumerable<Store>> All()
        {
                return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
                var entitieToDelete = await dbSet.FindAsync(id);

                dbSet.Remove(entitieToDelete);
                return true;
        }

        public async Task<bool> Update(Store store)
        {
                var entityToUpsert = await dbSet.FindAsync(store.Id);

                entityToUpsert.Name= store.Name;

                return true;
        }
    }
}

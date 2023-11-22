using BackendEntities.Entities;
using BackendHelper.Repositories.IRepos;
using Microsoft.EntityFrameworkCore;
using BackendEntities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace BackendHelper.Repositories.repos
{
    public class ManufactureRepository : GenericRepository<Manufacture>, IManufactureRepo
    {
        public ManufactureRepository(ContextDb context, ILogger logger, IMapper mapper) : base(context, logger, mapper) { }

        public async Task<IEnumerable<Manufacture>> All()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var entityToDelete = await dbSet.FindAsync(id);

            dbSet.Remove(entityToDelete);
            return true;
        }

        public async Task<bool> Update(Manufacture manufacture)
        {
          var entityToUpsert = await dbSet.FindAsync(manufacture.Id);

                entityToUpsert.Name = manufacture.Name;

                return true;
        }
    }
}

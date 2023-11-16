using WebDb.Entities;
using WebDb.Repositories.IPostgres;
using Microsoft.EntityFrameworkCore;
using WebDb.Helpers;

namespace WebDb.Repositories.postgres
{
    public class ManufactureRepository : GenericRepository<Manufacture>, IManufactureRepo
    {
        public ManufactureRepository(ContextDb context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Manufacture>> All()
        {
            try
            {
                return await dbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} All function error", typeof(ManufactureRepository));
                return new List<Manufacture>();
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var entityToDelete = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (entityToDelete == null) { return false; }

                dbSet.Remove(entityToDelete);
                return true;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "{Repo} Delete function error", typeof(ManufactureRepository));
                return false;
            }
        }

        public override async Task<bool> Upsert(Manufacture entity)
        {
            try
            {
                var entityToUpsert = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (entityToUpsert == null) { return await Add(entity); }

                entityToUpsert.Name = entity.Name;
                return true;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "{Repo} Upsert function error", typeof(ManufactureRepository));
                return false;
            }
        }
    }
}

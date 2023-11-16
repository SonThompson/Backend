using WebDb.Entities;
using WebDb.Repositories.IPostgres;
using Microsoft.EntityFrameworkCore;
using WebDb.Helpers;

namespace WebDb.Repositories.postgres
{
    public class ProductRepository : GenericRepository<Product>, IProductRepo
    {
        public ProductRepository(ContextDb context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Product>> All()
        {
            try
            {
                return await dbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} All function error", typeof(ProductRepository));
                return new List<Product>();
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
                logger.LogError(ex, "{Repo} Delete function error", typeof(ProductRepository));
                return false;
            }
        }

        public override async Task<bool> Upsert(Product entity)
        {
            try
            {
                var entityToUpsert = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (entityToUpsert == null) { return await Add(entity); }

                entityToUpsert.CategoryId = entity.CategoryId;
                entityToUpsert.ManufactureId = entity.ManufactureId;
                entityToUpsert.Name = entity.Name;

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} Upsert function error", typeof(CustomerRepository));
                return false;
            }
        }
    }
}

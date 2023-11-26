using Backend.DAL.Entities;
using BackendHelper.Repositories.IRepos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Backend.DAL.Repositories.repos
{
    public class ProductRepository : GenericRepository<Product>, IProductRepo
    {
        public ProductRepository(ContextDb context, ILogger logger, IMapper mapper) : base(context, logger, mapper) { }

        public async Task<IEnumerable<Product>> All()
        {
                return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
                var entitieToDelete = await dbSet.FindAsync(id);

                dbSet.Remove(entitieToDelete);

                return true;
        }

        public async Task<bool> Update(Product product)
        {
                var entityToUpsert = await dbSet.FindAsync(product.Id);

                entityToUpsert.CategoryId = product.CategoryId;
                entityToUpsert.ManufactureId = product.ManufactureId;
                entityToUpsert.Name = product.Name;

                return true;
        }
    }
}

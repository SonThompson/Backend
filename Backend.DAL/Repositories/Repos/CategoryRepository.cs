using Backend.DAL.Entities;
using BackendHelper.Repositories.IRepos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Backend.DAL.Repositories.repos
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepo
    {
        public CategoryRepository(ContextDb context, ILogger logger, IMapper mapper) : base(context, logger, mapper) { }

        public async Task<IEnumerable<Category>> All()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var entitieToDelete = await dbSet.FindAsync(id);
            dbSet.Remove(entitieToDelete);

            return true;
        }

        public async Task<bool> Update(Category category)
        {
            var entityToUpsert = await dbSet.FindAsync(category.Id);

            entityToUpsert.Name = category.Name;

            return true;
        }
    }
}

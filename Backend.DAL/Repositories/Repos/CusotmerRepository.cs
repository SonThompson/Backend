using Backend.DAL.Entities;
using BackendHelper.Repositories.IRepos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Backend.DAL.Repositories.repos
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepo
    {
        public CustomerRepository(ContextDb context, ILogger logger, IMapper mapper) : base(context, logger, mapper) { }

        public async Task<IEnumerable<Customer>> All()
        {
                return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
                var entitieToDelete = await dbSet.FindAsync(id);

                dbSet.Remove(entitieToDelete);

                return true;
        }

        public async Task<bool> Update(Customer customer)
        {
                var entityToUpsert = await dbSet.FindAsync(customer.Id);

                entityToUpsert.Name = customer.Name;

                return true;
        }
    }
}

using WebDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDb.Repositories.IPostgres
{
    public interface IDeliveryRepo : IGenericRepository<Delivery>
    {
        Task<IEnumerable<Delivery>> AllWithSort(bool SortByCount, bool SortByDate);
    }
}

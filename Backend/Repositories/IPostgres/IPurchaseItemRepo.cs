using WebDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDb.Repositories.IPostgres
{
    public interface IPurchaseItemRepo : IGenericRepository<PurchaseItem>
    {
        Task<IEnumerable<PurchaseItem>> AllWithSort(bool SortByPrice, bool SortByCount);
    }
}

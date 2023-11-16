using WebDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace WebDb.Repositories.IPostgres
{
    public interface ICategoryRepo : IGenericRepository<Category>
    {
        //Task<Category> GetByName(string Name);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SingletonPattern_Log_CRUD.Models;

namespace SingletonPattern_Log_CRUD.Data
{
    public class SingletonPattern_Log_CRUDContext : DbContext
    {
        public SingletonPattern_Log_CRUDContext (DbContextOptions<SingletonPattern_Log_CRUDContext> options)
            : base(options)
        {
        }

        public DbSet<SingletonPattern_Log_CRUD.Models.Car> Car { get; set; } = default!;
    }
}

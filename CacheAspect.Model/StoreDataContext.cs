using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheAspect.Model
{
    public class StoreDataContext : DbContext, IUnitOfWork
    {
        public StoreDataContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public void Commit()
        {
            base.SaveChanges();
        }
    }
}

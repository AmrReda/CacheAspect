using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheAspect.Model; 

namespace CacheAspect.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(StoreDataContext context)
            : base(context)
        {
        }

        [Cache(CacheAction.Add)]
        public IEnumerable<Customer> GetAllByContactTitle(string contactTitle)
        {
            return GetAll(q => q.ContactTitle.Contains(contactTitle));
        }
    }
}

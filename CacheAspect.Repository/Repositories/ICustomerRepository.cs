using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheAspect.Model;

namespace CacheAspect.Repository
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        IEnumerable<Customer> GetAllByContactTitle(string contactTitle);
    }
}

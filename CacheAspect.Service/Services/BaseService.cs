using CacheAspect.Model;
using System;

namespace CacheAspect.Service
{
    public class BaseService : IDisposable
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public void Dispose()
        {
        }
    }
}

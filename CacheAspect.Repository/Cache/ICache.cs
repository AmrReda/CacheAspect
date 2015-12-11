using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheAspect.Repository
{
    /// <summary>
    /// Cache interface.
    /// </summary>
    /// <remarks>
    /// This class was based on Matthew Groves' original ICache class.
    /// https://github.com/mgroves/PostSharp5/blob/master/4-Caching/ICache.cs
    /// </remarks>
    public interface ICache
    {
        object this[string key] { get; set; }

        void Remove(string key);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoQuo.Caching.Interfaces
{
    public interface ICached
    {
        bool Add<T>(string key, T item, int expireInMinute = 0);
        bool Remove(string key);
        T Get<T>(string key);
    }
}

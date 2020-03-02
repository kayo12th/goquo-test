using GoQuo.Caching.DTO;
using GoQuo.Caching.DTO.Entities;
using GoQuo.Caching.Enums;
using GoQuo.Caching.Interfaces;
using GoQuo.Caching.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoQuo.Caching.Implements
{
    public class CachingBo : ICachingBo
    {
        private CachedEnum.CachedTypes _dbContextType = CachedEnum.CachedTypes.Redis;
        private ICached _cached;

        public CachingBo()
        {
            _cached = SwitchCachedType(_dbContextType);
        }
        public CachingBo(CachedEnum.CachedTypes dbContextType)
        {
            _dbContextType = dbContextType;
            _cached = SwitchCachedType(_dbContextType);
        }

        public bool Add<T>(string key, T item, int expireInMinute = 0)
        {
            return _cached.Add(key, item, expireInMinute);
        }

        public T Get<T>(string key)
        {
            return _cached.Get<T>(key);
        }

        public bool Remove(string key)
        {
            return _cached.Remove(key);
        }

        private ICached SwitchCachedType(CachedEnum.CachedTypes dbContextType)
        {
            return CachingFactory.Instance.GetCachedType(dbContextType);
        }
    }
}

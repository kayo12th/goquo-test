using GoQuo.Caching.Enums;
using GoQuo.Caching.Implements;
using GoQuo.Caching.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoQuo.Caching.DTO
{
    public class CachingFactory
    {
        private readonly Hashtable _cachedMapping = new Hashtable();
        private static readonly object LockObject = new object();
        private static CachingFactory _instance;

        private CachingFactory()
        {
            Initialize();
        }

        public static CachingFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        if (_instance == null)
                            _instance = new CachingFactory();
                    }
                }

                return _instance;
            }
        }

        public void Initialize()
        {
            //Redis
            _cachedMapping[CachedEnum.CachedTypes.Redis] = typeof(RedisCached);
            //IIS
            _cachedMapping[CachedEnum.CachedTypes.IIS] = typeof(IISCached);
        }

        public ICached GetCachedType(CachedEnum.CachedTypes cachedType)
        {
            ICached cached = null;

            if (_cachedMapping.ContainsKey(cachedType))
            {
                cached = Activator.CreateInstance((Type)_cachedMapping[cachedType]) as ICached;
            }

            return cached;
        }
    }
}

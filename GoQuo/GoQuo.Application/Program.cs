using GoQuo.Caching.Enums;
using GoQuo.Caching.Implements;
using GoQuo.Caching.Interfaces;
using GoQuo.Caching.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoQuo.Application
{
    class Program
    {
        private static string keyCached = "TestKeycached";
        private static int cacheTime = 60;
        private static int cacheType = AppSettings.Instance.GetInt32("CachedType", (int)CachedEnum.CachedTypes.Redis);
        private static ICachingBo _cachingBo;
        static void Main(string[] args)
        {
            switch (cacheType)
            {
                case (int)CachedEnum.CachedTypes.Redis:
                    _cachingBo = new CachingBo(CachedEnum.CachedTypes.Redis);
                    break;
                default:
                case (int)CachedEnum.CachedTypes.IIS:
                    _cachingBo = new CachingBo(CachedEnum.CachedTypes.IIS);
                    break;
            }

            string message = "This is a test message!";

            //add cache 
            _cachingBo.Add(keyCached, message, cacheTime);

            //get cache 
            string data = _cachingBo.Get<string>(keyCached);

            Console.WriteLine(data);
        }
    }
}

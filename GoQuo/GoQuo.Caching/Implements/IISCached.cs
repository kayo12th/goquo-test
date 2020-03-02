using GoQuo.Caching.Enums;
using GoQuo.Caching.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace GoQuo.Caching.Implements
{
    public class IISCached : ICached
    {
        public IISCached()
        {
        }
        public bool Add<T>(string key, T item, int expireInMinute)
        {
            try
            {
                if (item == null || string.IsNullOrEmpty(key)) return false;

                HttpRuntime.Cache.Insert(key, item, null, DateTime.Now.AddMinutes(expireInMinute), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Remove(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key)) return true;
                HttpRuntime.Cache.Remove(key);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public T Get<T>(string key)
        {
            try
            {
                object data = HttpRuntime.Cache[key];
                if (null != data)
                {
                    try
                    {
                        return (T)data;
                    }
                    catch
                    {
                        return default(T);
                    }
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(T);
            }
        }
    }
}

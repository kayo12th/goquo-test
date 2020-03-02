using GoQuo.Caching.DTO.Entities;
using GoQuo.Caching.Interfaces;
using GoQuo.Caching.Utilities;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GoQuo.Caching.Implements
{
    public class RedisCached : ICached
    {
        private CachingConfigModel _configuration;

        public RedisCached()
        {
            this._configuration = new CachingConfigModel()
            {
                IpServer = AppSettings.Instance.GetString("RedisIP"),
                Port = AppSettings.Instance.GetInt32("RedisPort"),
                DB = AppSettings.Instance.GetInt32("RedisDB"),
                ConnectTimeout = AppSettings.Instance.GetInt32("RedisTimeout", 600),
                RedisSlotNameInMemory = AppSettings.Instance.GetString("RedisSlotName", "Goquo")
            };
        }

        public RedisCached(CachingConfigModel configuration)
        {
            if (configuration.ConnectTimeout > 0)
                configuration.ConnectTimeout = configuration.ConnectTimeout;
            this._configuration = configuration;
        }

        private IRedisClient CreateInstance()
        {
            IRedisClient client;

            try
            {
                HttpContext context = HttpContext.Current;

                if (context != null)
                {
                    if (!context.Items.Contains(_configuration.RedisSlotNameInMemory))
                    {
                        client = new RedisClient(_configuration.IpServer, _configuration.Port)
                        {
                            Db = _configuration.DB,
                            ConnectTimeout = _configuration.ConnectTimeout
                        };

                        context.Items.Add(_configuration.RedisSlotNameInMemory, client);
                    }

                    return (RedisClient)context.Items[_configuration.RedisSlotNameInMemory];
                }
                else
                {
                    client = new RedisClient(_configuration.IpServer, _configuration.Port)
                    {
                        Db = _configuration.DB,
                        ConnectTimeout = _configuration.ConnectTimeout
                    };
                }

            }
            catch (Exception)
            {
                client = new RedisClient(_configuration.IpServer, _configuration.Port)
                {
                    Db = _configuration.DB,
                    ConnectTimeout = _configuration.ConnectTimeout
                };
            }

            return client;
        }


        public bool Add<T>(string key, T item, int expireInMinute = 0)
        {
            bool result = false;
            try
            {
                using (IRedisClient client = CreateInstance())
                {
                    if (expireInMinute > 0)
                        result = client.Set<T>(key, item, DateTime.Now.AddMinutes(expireInMinute));
                    else
                        result = client.Set<T>(key, item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Set<T> Key: {0} {1} {2}", key, Environment.NewLine, ex.ToString()));
                return false;
            }
            return result;
        }

        public T Get<T>(string key)
        {
            T result = default(T);
            try
            {
                using (IRedisClient client = CreateInstance())
                {
                    result = client.Get<T>(key);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Set<T> Key: {0} {1} {2}", key, Environment.NewLine, ex.ToString()));
            }
            return result;
        }

        public bool Remove(string key)
        {
            bool result = false;

            try
            {
                using (IRedisClient client = CreateInstance())
                {
                    result = client.Remove(key);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }
    }
}

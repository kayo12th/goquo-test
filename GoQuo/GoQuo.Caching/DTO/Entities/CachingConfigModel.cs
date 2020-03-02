using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoQuo.Caching.DTO.Entities
{
    public class CachingConfigModel
    {
        public string IpServer { get; set; }
        public int Port { get; set; }
        public int DB { get; set; }
        public int ConnectTimeout { get; set; }
        public string RedisSlotNameInMemory { get; set; }
    }
}

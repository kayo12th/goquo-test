using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoQuo.Caching.Enums
{
    public class CachedEnum
    {
        public enum CachedTypes
        {
            /// <summary>
            /// Redis = 1
            /// </summary>
            Redis = 1,

            /// <summary>
            /// MSSQL = 2
            /// </summary>
            IIS = 2,
        }
    }
}

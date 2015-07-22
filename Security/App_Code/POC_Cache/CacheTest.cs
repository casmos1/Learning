using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Yoda
{

    /// <summary>
    /// Summary description for CacheTest
    /// </summary>
    public class CacheTest
    {
        private const string BearerKey = "Bearer";
        private const int DefaultExpirationDurationInSeconds = 3540;

        [DefaultValue(DefaultExpirationDurationInSeconds)]
        public int  ExpirationDurationInSeconds{ get; set; }

        public object GetDrxBearerToken()
        {
            object bearerToken = "Cache some object in here";

            if (HttpRuntime.Cache.Get(BearerKey) != null) // bearer token exists
            {
                bearerToken = HttpRuntime.Cache.Get(BearerKey);
            }
            else
            {
                HttpRuntime.Cache.Add(BearerKey, bearerToken, null,
                    DateTime.Now.AddSeconds(DefaultExpirationDurationInSeconds),
                    Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }

            return bearerToken;
        }

    }
}

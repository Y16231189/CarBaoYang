using CarBaoYang.Unit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;

namespace CarBaoYang.Cache
{
    public class ManufacturerManage
    {
        private const string cachekey = "_ManufacturerCache_";
        private static DateTime absolutetime = DateTime.MaxValue;
        private static TimeSpan slidingtime = new TimeSpan(6, 0, 0);
        private static List<ResultJson.manufacturer> temp_data = new List<ResultJson.manufacturer>();
        static ManufacturerManage()
        {
            WebCache.Max(cachekey, GetData(), null, absolutetime, slidingtime, UpdateBrandCacheCallBack);
        }

        private static void UpdateBrandCacheCallBack(string key, System.Web.Caching.CacheItemUpdateReason reason, out object expensiveObject, out  System.Web.Caching.CacheDependency dependency, out DateTime absoluteExpiration, out TimeSpan slidingExpiration)
        {
            expensiveObject = GetData();
            dependency = null;
            absoluteExpiration = absolutetime;
            slidingExpiration = slidingtime;
        }

        static List<ResultJson.manufacturer> GetData()
        {
            if (Config.conn.State != ConnectionState.Open)
            {
                Config.conn.Open();
            }
            string sqlCmd = "select * from manufacturer";
            IList<ResultJson.manufacturer> value = Config.conn.Query<ResultJson.manufacturer>(sqlCmd).ToList();
            Config.conn.Close();
            if (value != null && value.Count > 0)
            {
                temp_data.Clear();
                temp_data.AddRange(value);
            }
            return temp_data;
        }
        public static IList<ResultJson.manufacturer> Data
        {
            get
            {
                return WebCache.Get(cachekey) as IList<ResultJson.manufacturer>;
            }
        }
    }
}
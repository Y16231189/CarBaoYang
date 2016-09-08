using CarBaoYang.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
namespace CarBaoYang.Cache
{
    public class BrandManage
    {
        private const string cachekey = "_brandCache_";
        private static DateTime absolutetime = DateTime.MaxValue;
        private static TimeSpan slidingtime = new TimeSpan(6, 0, 0);
        private static List<ResultJson.Brands> temp_data = new List<ResultJson.Brands>();
        static BrandManage()
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

        static List<ResultJson.Brands> GetData()
        {
            if (Config.conn.State != ConnectionState.Open)
            {
                Config.conn.Open();
            }
            string sqlCmd = "select * from brand";
            IList<ResultJson.Brands> value = Config.conn.Query<ResultJson.Brands>(sqlCmd).ToList();
            Config.conn.Close();
            if (value != null && value.Count > 0)
            {
                temp_data.Clear();
                temp_data.AddRange(value);
            }
            return temp_data;
        }
        public static IList<ResultJson.Brands> Data
        {
            get
            {
                return WebCache.Get(cachekey) as IList<ResultJson.Brands>;
            }
        }
    }
}
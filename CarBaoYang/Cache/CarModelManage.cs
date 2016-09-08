using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using CarBaoYang.Unit;

namespace CarBaoYang.Cache
{
    public class CarModelManage
    { private const string cachekey = "_CarModelCache_";
        private static DateTime absolutetime = DateTime.MaxValue;
        private static TimeSpan slidingtime = new TimeSpan(6, 0, 0);
        private static List<ResultJson.CarModel> temp_data = new List<ResultJson.CarModel>();
        static CarModelManage()
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

        static List<ResultJson.CarModel> GetData()
        {
            if (Config.conn.State != ConnectionState.Open)
            {
                Config.conn.Open();
            }
            string sqlCmd = "select * from model";
            IList<ResultJson.CarModel> value = Config.conn.Query<ResultJson.CarModel>(sqlCmd).ToList();
            Config.conn.Close();
            if (value != null && value.Count > 0)
            {
                temp_data.Clear();
                temp_data.AddRange(value);
            }
            return temp_data;
        }
        public static IList<ResultJson.CarModel> Data
        {
            get
            {
                return WebCache.Get(cachekey) as IList<ResultJson.CarModel>;
            }
        }
    }
}
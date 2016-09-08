using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CarBaoYang.Unit
{
    public partial class WebCache
    {
        #region Fields
        private static readonly System.Web.Caching.Cache _cache;
        #endregion

        #region Ctor
        /// <summary>
        /// Creates a new instance of the NopCache class
        /// </summary>
        static WebCache()
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                _cache = current.Cache;
            }
            else
            {
                _cache = HttpRuntime.Cache;
            }
        }

        /// <summary>
        /// Creates a new instance of the NopCache class
        /// </summary>
        private WebCache()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Removes all keys and values from the cache
        /// </summary>
        public static void Clear()
        {
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                _cache.Remove(enumerator.Key.ToString());
            }
        }
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public static object Get(string key)
        {
            return _cache[key];
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">object</param>
        public static void Max(string key, object obj)
        {
            Max(key, obj, null);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">object</param>
        /// <param name="dep">cache dependency</param>
        public static void Max(string key, object obj, System.Web.Caching.CacheDependency dep)
        {
            Max(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, null);
        }
        public void Max(string key, object obj, System.Web.Caching.CacheDependency dep, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            Max(key, obj, dep, absoluteExpiration, slidingExpiration, null);
        }
        public static void Max(string key, object obj, System.Web.Caching.CacheDependency dep, DateTime absoluteExpiration, TimeSpan slidingExpiration, System.Web.Caching.CacheItemUpdateCallback updateCallback)
        {
            if ((obj != null))
            {
                if (updateCallback == null)
                {
                    _cache.Insert(key, obj, dep, absoluteExpiration, slidingExpiration);
                }
                else
                {
                    _cache.Insert(key, obj, dep, absoluteExpiration, slidingExpiration, updateCallback);
                }
            }
        }



        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public static void RemoveByPattern(string pattern)
        {
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Key.ToString()))
                {
                    _cache.Remove(enumerator.Key.ToString());
                }
            }
        }
        #endregion


    }
}
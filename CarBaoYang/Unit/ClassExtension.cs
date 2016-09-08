using System;
using Newtonsoft.Json;
using EmitMapper;

namespace CarBaoYang
{
    public static class ClassExtension
    {
        public static string ToJsonString<T>(this T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        public static T StringToT<T>(this string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }

        /// <summary>
        /// 强制转换指定类
        /// </summary>
        /// <typeparam name="TTo">目标结果类</typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TTo ToMapper<TTo>(this object obj)
            where TTo : class
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapperImpl(obj.GetType(), typeof(TTo), EmitMapper.MappingConfiguration.DefaultMapConfig.Instance);
            return mapper.Map(obj) as TTo;
        }
    }
}


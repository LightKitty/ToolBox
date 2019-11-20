using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox
{
    public static class TTime
    {
        private static readonly DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区起始计算时间

        /// <summary>
        /// 获取当前JavaScript时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetJsTimestampNow()
        {
            return GetJsTimestamp(DateTime.Now);
        }

        /// <summary>
        /// 获取JavaScript时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetJsTimestamp(DateTime time)
        {
            long timeStamp = (long)(time - startTime).TotalMilliseconds; // 相差毫秒数
            return timeStamp;
        }

        /// <summary>
        /// js时间戳转DateTime
        /// </summary>
        /// <param name="jsTimestamp"></param>
        /// <returns></returns>
        public static DateTime JsTimestampToDatetime(long jsTimestamp)
        {
            return startTime.AddMilliseconds(jsTimestamp);
        }

        /// <summary>
        /// 获取当前时间Unix时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetUnixTimestampNow()
        {
            return GetUnixTimestamp(DateTime.Now);
        }

        /// <summary>
        /// 获取Unix时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long GetUnixTimestamp(DateTime time)
        {
            long timeStamp = (long)(time - startTime).TotalSeconds; // 相差毫秒数
            return timeStamp;
        }

        /// <summary>
        /// unix时间戳转DateTime
        /// </summary>
        /// <param name="unixTimestamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDatetime(long unixTimestamp)
        {
            return startTime.AddSeconds(unixTimestamp);
        }
    }
}

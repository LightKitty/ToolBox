using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox
{
    /// <summary>
    /// 类型转换工具
    /// </summary>
    public static class TConvert
    {
        /// <summary>
        /// 使用默认编码获取字符串
        /// </summary>
        /// <param name="byteArr">字节数组</param>
        /// <returns>字符串</returns>
        public static string GetString(byte[] byteArr)
        {
            return GetString(byteArr);
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="byteArr">字节数组</param>
        /// <param name="encoding">编码</param>
        /// <returns>字符串</returns>
        public static string GetString(byte[] byteArr, Encoding encoding)
        {
            return encoding.GetString(byteArr);
        }
    }
}

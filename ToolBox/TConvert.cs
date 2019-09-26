using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        /// <param name="bytes">字节数组</param>
        /// <returns>字符串</returns>
        public static string BytesToString(byte[] bytes)
        {
            return BytesToString(bytes, TCommon.defultEncoding);
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="encoding">编码</param>
        /// <returns>字符串</returns>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 字符串转字节数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] StringToBytes(string str)
        {
            return StringToBytes(str, TCommon.defultEncoding);
        }

        /// <summary>
        /// 字符串转字节数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns>字节数组</returns>
        public static byte[] StringToBytes(string str,Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        /// <summary>
        /// 对象转字节数组
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>字节数组</returns>
        public static byte[] ObjectToBytes(object obj)
        {
            System.IO.MemoryStream _memory = new System.IO.MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(_memory, obj);
            _memory.Position = 0;
            byte[] read = new byte[_memory.Length];
            _memory.Read(read, 0, read.Length);
            _memory.Close();
            return read;
        }

        /// <summary>
        /// 字节数组转对象（二进制序列化）
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>object</returns>
        public static T BytesToObject<T>(byte[] bytes)
        {
            System.IO.MemoryStream _memory = new System.IO.MemoryStream(bytes)
            {
                Position = 0
            };
            BinaryFormatter formatter = new BinaryFormatter();
            object _newOjb = formatter.Deserialize(_memory);
            _memory.Close();
            return (T)_newOjb;
        }

        /// <summary>
        /// 对象转json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// json转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}

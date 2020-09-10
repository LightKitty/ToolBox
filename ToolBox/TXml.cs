using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ToolBox
{
    public static class TXml
    {
        /// <summary>
        /// 读取Xml文档
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static XmlDocument Load(string path)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(path);
            return xmldoc;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Serialize(object data)
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xz = new XmlSerializer(data.GetType());
                xz.Serialize(sw, data);
                return sw.ToString();
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string path)
        {
            return Deserialize<T>(path, Encoding.UTF8);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string path, Encoding encoding)
        {
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                StreamReader sr = new StreamReader(fs, encoding);
                XmlSerializer xz = new XmlSerializer(typeof(T));
                return (T)xz.Deserialize(sr);
            }
        }
    }
}

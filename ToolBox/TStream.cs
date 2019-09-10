using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToolBox
{
    public static class TStream
    {
        /// <summary>
        /// 获取本地文件所有字符串
        /// </summary>
        /// <param name="path">全路径</param>
        /// <returns>字符串</returns>
        public static string GetAllStrFromLocalFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string str = sr.ReadToEnd();
            sr.Close();
            return str;
        }

        /// <summary>
        /// 获取本地文件字节流
        /// </summary>
        /// <param name="path">全路径</param>
        /// <returns>字节流</returns>
        public static Stream GetLocalFileStream(string path)
        {
            return new MemoryStream(GetLocalFileByteArr(path));
        }

        /// <summary>
        /// 获取本地文件字节数组
        /// </summary>
        /// <param name="path">全路径</param>
        /// <returns>字节数组</returns>
        public static byte[] GetLocalFileByteArr(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);           //获取流对象
            BinaryReader br = new BinaryReader(fs);                              //二进制读取
            return br.ReadBytes((int)br.BaseStream.Length);
        }
    }
}

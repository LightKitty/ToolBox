using System.IO;

namespace ToolBox
{
    /// <summary>
    /// 文件读取
    /// </summary>
    public static class TStream
    {
        /// <summary>
        /// 获取本地文件所有字符串
        /// </summary>
        /// <param name="path">全路径</param>
        /// <returns>字符串</returns>
        public static string GetFileString(string path)
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
        public static Stream GetFileStream(string path)
        {
            return new MemoryStream(GetFileByteArr(path));
        }

        /// <summary>
        /// 获取文件字节数组
        /// </summary>
        /// <param name="path">全路径</param>
        /// <returns>字节数组</returns>
        public static byte[] GetFileByteArr(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open); //获取流对象
            BinaryReader br = new BinaryReader(fs); //二进制读取
            return br.ReadBytes((int)br.BaseStream.Length);
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="dataBytes">字节数组</param>
        /// <param name="path">路径</param>
        public static void SaveFile(byte[] dataBytes, string path)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(dataBytes);
        }
    }
}
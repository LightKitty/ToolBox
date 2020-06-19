using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox
{
    /// <summary>
    /// MD5加密解密
    /// </summary>
    public static class TMD5
    {
        private static MD5 md5 = MD5.Create();//MD5抽象类无法实例化 实例化该方法
        public static string GenerateMD5(string str, Encoding encode)
        {
            byte[] buffer = encode.GetBytes(str); //将字符串转换为字节数组
            byte[] mdbuffer = md5.ComputeHash(buffer); //调用ComputeHash方法把数组传进去
            StringBuilder result = new StringBuilder(); //将字节数组中每个元素ToString();
            for (int i = 0; i < mdbuffer.Length; i++)
            {
                result.Append(mdbuffer[i].ToString("x2")); //ToString得到十进制字符串 ToString("x")十六进制字符串 ToString("x2")对齐
            }
            return result.ToString();
        }
    }
}

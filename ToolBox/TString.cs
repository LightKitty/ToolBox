using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ToolBox
{
    /// <summary>
    /// 字符串处理
    /// </summary>
    public static class TString
    {
        /// <summary>
        /// 替换string中的符号
        /// </summary>
        /// <param name="s">需要替换的string</param>
        /// <param name="replacement">替换为字符串</param>
        /// <returns>替换后结果</returns>
        public static string ReplaceAllSymbol(string s,string replacement)
        {
            return Regex.Replace(s, "[^\u4e00-\u9fa50-9a-zA-Z]+", replacement);
        }

        /// <summary>
        /// 获取base64编码
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encodingName"></param>
        /// <returns></returns>
        public static string ToBase64String(string s, string encodingName = "utf-8")
        {
            Encoding encode = Encoding.GetEncoding(encodingName);
            return ToBase64String(s, encode);
        }

        /// <summary>
        /// 获取base64编码
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string ToBase64String(string s, Encoding encode)
        {
            return Convert.ToBase64String(encode.GetBytes(s));
        }

        /// <summary>
        /// Unicode解码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string DecodeUnicode(string s)
        {
            Regex reUnicode = new Regex(@"\\u([0-9a-fA-F]{4})", RegexOptions.Compiled);

            return reUnicode.Replace(s, m =>
            {
                short c;
                if (short.TryParse(m.Groups[1].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out c))
                {
                    return "" + (char)c;
                }
                return m.Value;
            });
        }
    }
}

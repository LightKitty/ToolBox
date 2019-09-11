using System;
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
        /// <param name="input">需要替换的string</param>
        /// <param name="replacement">替换为字符串</param>
        /// <returns>替换后结果</returns>
        public static string ReplaceAllSymbol(string input,string replacement)
        {
            return Regex.Replace(input, "[^\u4e00-\u9fa50-9a-zA-Z]+", "");
        }
    }
}

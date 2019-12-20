using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using ToolBox;
using System.Net;
using System.IO;
using System.Text;

namespace ToolBoxTest
{
    /// <summary>
    /// CommonTest 的摘要说明
    /// </summary>
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void DictionaryTest()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<int> list = new List<int>();
            Dictionary<int, int> dic = new Dictionary<int, int>();
            int count = 10000;
            int key = count / 2;
            int[] arr = new int[count];
            sw.Stop();

            sw.Restart();
            for (int i = 0; i < count; i++)
            {
                arr[i] = i;
            }
            sw.Stop();
            TimeSpan tt1 = sw.Elapsed;

            sw.Restart();
            for (int i = 0; i < count; i++)
            {
                list.Add(i);
            }
            sw.Stop();
            TimeSpan tt2 = sw.Elapsed;

            sw.Restart();
            for (int i = 0; i < count; i++)
            {
                dic.Add(i, i);
            }
            sw.Stop();
            TimeSpan tt3 = sw.Elapsed;

            //字典
            sw.Restart();
            for (int j = 0; j < 100; j++)
            {
                int index2 = dic[key];
            }
            sw.Stop();
            TimeSpan ts0 = sw.Elapsed;

            //数组 遍历
            sw.Restart();
            for (int j = 0; j < 100; j++)
            {
                for (int i = 0; i < count; i++)
                    if (arr[i] == key) break;
            }
            sw.Stop();
            TimeSpan ts11 = sw.Elapsed;

            //数组 迭代器
            sw.Restart();
            for (int j = 0; j < 100; j++)
            {
                var p = arr.GetEnumerator();
                while (p.MoveNext())
                {
                    if ((int)p.Current == key)
                        break;
                }
            }
            sw.Stop();
            TimeSpan ts12 = sw.Elapsed;

            //列表 遍历
            sw.Restart();
            for (int j = 0; j < 100; j++)
            {
                for (int i = 0; i < count; i++)
                    if (list[i] == key) break;
            }
            sw.Stop();
            TimeSpan ts21 = sw.Elapsed;

            //列表 Find
            sw.Restart();
            for (int j = 0; j < 100; j++)
            {
                list.Find(x => x == key);
            }
            sw.Stop();
            TimeSpan ts22 = sw.Elapsed;

            //列表 迭代器
            sw.Restart();
            for (int j = 0; j < 100; j++)
            {
                var q = list.GetEnumerator();
                while (q.MoveNext())
                {
                    if (q.Current == key)
                        break;
                }
            }
            sw.Stop();
            TimeSpan ts23 = sw.Elapsed;
        }

        [TestMethod]
        public void ZP58AuthTest()
        {
            string appKey = "b13e50cf44a0638c1b10618e2b845160";
            string timespan = TTime.GetJsTimestampNow().ToString();
            //string secret = "9792e56d8cf4a5535d5e3f631e6c1835";
            string url = $"https://openapi.58.com/v2/auth/show?app_key={ appKey }&redirect_uri={ "http://www.baidu.com/" }&state={ timespan }";

            var request = WebRequest.Create("https://openapi.58.com/v2/auth/show?app_key=b13e50cf44a0638c1b10618e2b845160&scopes=1,2,3,4,6&redirect_uri=http://recruitresume.tms.beisen.com/api/Channel/AuthCallback&state=H4sIAAAAAAAEAD3LsQ5AMBSF4Xc5c4cSEro1JgsLD9DETdqkLtGaxLtTwvjlP+fAQGw4thNUJmUmc4Ex0PZYoLGGmXxSUf/U3pkAhbIaKEQI9Ctx2vDu/fvvzEyf+xVKCug92pFds0x3Ac4LsCEOrnsAAAA=");
            var response = request.GetResponse();

            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                var result1 = reader.ReadToEnd();
            }

            var result = THttp.SimpleGetString(url);
        }

        [TestMethod]
        public void Z58GetPhone()
        {
            string appKey = "b13e50cf44a0638c1b10618e2b845160";
            string timespan = TTime.GetJsTimestampNow().ToString();
            var request = WebRequest.Create($"https://openapi.58.com/v3/zhaopin/getaxbphone?code=6G84O0169514E4741C0A3F62FE6C30&phoneNumber=13520881111");
            var response = request.GetResponse();

            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                var result1 = reader.ReadToEnd();
            }
        }
    }
}


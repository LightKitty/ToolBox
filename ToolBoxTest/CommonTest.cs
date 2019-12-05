using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

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
            for(int j=0;j<100;j++)
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
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox;

namespace ToolBoxTest
{
    [TestClass]
    public class TCopyTest
    {
        [TestMethod]
        public void DeepCopyTest()
        {
            DateTime startTime = DateTime.Now;
            TimeSpan st = Process.GetCurrentProcess().Threads[0].UserProcessorTime;
            List<string> list1 =new List<string>() { "a", "b", "c" };
            List<string> list2 = TCopy.DeepCopy(list1);
            list1[0] = "?";
            Assert.AreEqual(list2[0], "a");
            TimeSpan duration = Process.GetCurrentProcess().TotalProcessorTime;
            TimeSpan duration2 = DateTime.Now - startTime;
            TimeSpan d3 = Process.GetCurrentProcess().Threads[0].UserProcessorTime.Subtract(st);
        }
    }
}

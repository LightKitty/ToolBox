using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToolBox;

namespace ToolBoxTest
{
    /// <summary>
    /// TTimeTest 的摘要说明
    /// </summary>
    [TestClass]
    public class TTimeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            long jsTime = 1608465145812;
            DateTime time = TTime.JsTimestampToDatetime(jsTime);
        }
    }
}

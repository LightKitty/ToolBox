using System;
using ToolBox;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;

namespace ToolBoxTest
{
    [TestClass]
    public class THttpTest
    {
        [TestMethod]
        public void SimplePostStringTest()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                {"text","1" },
                {"value","2" },
            };
            var result = THttp.SimplePostString("http://127.0.0.1:8080", dic);
        }
    }
}

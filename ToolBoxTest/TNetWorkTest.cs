using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox;

namespace ToolBoxTest
{
    [TestClass]
    public class TNetWorkTest
    {
        [TestMethod]
        public void GetIpAddressTest()
        {
            var result = TNetWork.GetIpAddress();
            result = TNetWork.GetIpAddress();
            result = TNetWork.GetIpAddress(true);
        }
    }
}

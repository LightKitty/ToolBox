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
    public class TNetTest
    {
        [TestMethod]
        public void GetIpAddressTest()
        {
            var result = TNet.GetIpAddress();
            result = TNet.GetIpAddress();
            result = TNet.GetIpAddress(true);
        }
    }
}

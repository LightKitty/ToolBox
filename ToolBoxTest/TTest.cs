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
    public class TTest
    {
        [TestMethod]
        public void Test1()
        {
            byte[] byteArr = TStream.GetFileByteArr("F:\\华为手机备忘录.txt");
            string text = TConvert.BytesToString(byteArr);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ToolBox;

namespace ToolBoxTest
{
    [TestClass]
    public class TZipTest
    {
        [TestMethod]
        public void UnZipTest()
        {
            string zipPath = "d:\\哈哈.zip";
            string outPath = "d:\\我";
            bool result = TZip.UnZip(zipPath, outPath);
            Assert.IsTrue(result);
        }
    }
}

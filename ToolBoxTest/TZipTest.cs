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
            string zipPath = "d:\\0.zip";
            string outPath = "d:\\1";
            bool result = TZip.UnZip(zipPath, outPath);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreatZipTest()
        {
            List<string> paths = new List<string>()
            {
                "d:\\1\\2",
                "d:\\1\\3.zip",
                "d:\\1\\4.xmind",
                "d:\\1\\5.xlsx"
            };
            string outPath = "d:\\0.zip";
            bool result = TZip.CreateZip(paths, outPath);
            Assert.IsTrue(result);
        }
    }
}

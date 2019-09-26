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
    public class TConvertTest
    {
        [TestMethod]
        public void ObjectToBytesTest()
        {
            string[] arr = new string[] { "1", "2" };
            var result = TConvert.ObjectToBytes(arr);
            var result2 = TConvert.BytesToObject<List<string>>(result);
        }

        [TestMethod]
        public void ObjectToJsonTest()
        {
            string[] arr = new string[] { "1", "2" };
            var result = TConvert.ObjectToJson(arr);
            var result2 = TConvert.JsonToObject<List<string>>(result);
        }
    }
}

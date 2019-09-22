using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ToolBox;

namespace ToolBoxTest
{
    [TestClass]
    public class TAesTest
    {
        [TestMethod]
        public void EncryptTest()
        {
            string result = TAes.Encrypt("汉语", "hello");
            Assert.AreEqual(result, "lb0rbR1W+FBpAwWTTF8DIQ==");
        }

        [TestMethod]
        public void DecryptTest()
        {
            string result = TAes.Decrypt("lb0rbR1W+FBpAwWTTF8DIQ==", "hello");
            Assert.AreEqual(result, "汉语");
        }
    }
}

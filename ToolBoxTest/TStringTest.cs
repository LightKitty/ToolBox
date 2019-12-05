using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using ToolBox;

namespace ToolBoxTest
{
    [TestClass]
    public class TStringTest
    {
        [TestMethod]
        public void ReplaceAllSymbolTest()
        {
            string input = "¹þ?hello|world,";
            string result = TString.ReplaceAllSymbol(input, "");
            Assert.AreEqual(result, "¹þhelloworld");
        }

        [TestMethod]
        public void Test()
        {
            var buffer = TStream.GetFileByteArr(@"d:\¹ÅÊ«´ÊÃû¾ä¡¾¹Ù·½ÍÆ¼ö¡¿.scel");
            string txt = TConvert.BytesToString(buffer, Encoding.Default);
        }
    }
}

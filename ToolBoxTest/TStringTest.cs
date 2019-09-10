using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}

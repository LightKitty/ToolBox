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
            string input = "��?hello|world,";
            string result = TString.ReplaceAllSymbol(input, "");
            Assert.AreEqual(result, "��helloworld");
        }

        [TestMethod]
        public void Test()
        {
            var buffer = TStream.GetFileByteArr(@"d:\��ʫ�����䡾�ٷ��Ƽ���.scel");
            string txt = TConvert.BytesToString(buffer, Encoding.Default);
        }
    }
}

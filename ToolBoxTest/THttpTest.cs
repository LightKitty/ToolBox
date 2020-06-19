using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ToolBoxTest
{
    [TestClass]
    public class THttpTest
    {
        [TestMethod]
        public void SimplePostStringTest()
        {
            List<int> personIds = new List<int> { 154500 };
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "personIds",Newtonsoft.Json.JsonConvert.SerializeObject(personIds)}
            };
            var result = ToolBox.THttp.SimplePostString("http://recruitapi.tms.beisen.net/api/v1/100102/0/applicant/getelinkbatch?toUserId=154500&anchorTag=interview", dic);
        }

        [TestMethod]
        public void Test()
        {
            List<int> personIds = new List<int> { 154500 };
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "personIdsStr","154500" }
            };
            string personIdsStr = Newtonsoft.Json.JsonConvert.SerializeObject(personIds);
            
            var result = ToolBox.THttp.SimpleGetString($"http://recruitapi.tms.beisen.net/api/v1/100102/0/applicant/getelinkbatch?toUserId=154500&anchorTag=interview&personIds=154500&personIds=154501&personIds=154502");
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox
{
    public static class TJson
    {
        public static void Read()
        {
            string text = TStream.GetFileString(@"C:\Users\yangguanghui\Desktop\1.js");
            List<List<object>> list= JsonConvert.DeserializeObject<List<List<object>>>(text);
            List<ZhiWei> result = new List<ZhiWei>();
            foreach(var u in list)
            {
                result.Add(new ZhiWei
                {
                    Id = u[0].ToString(),
                    Names = JsonConvert.DeserializeObject<List<string>>(u[1].ToString()),
                    ParentId = u[2].ToString(),
                    Index = Convert.ToInt32(u[3])
                });
            }

            List<ZhiWei> nodes1 = result.FindAll(x => x.ParentId == "");
            //Nodes1.Sort((x, y) => Convert.ToInt32(x.Id).CompareTo(Convert.ToInt32(y.Id)));
            StreamWriter sw = new StreamWriter("d:\\result.csv", false, Encoding.UTF8);
            foreach(var node1 in nodes1)
            {
                sw.WriteLine(node1.Names.FirstOrDefault()?.ToString());
                List<ZhiWei> childs = result.FindAll(x => x.ParentId == node1.Id);
                string t = string.Join(",", childs.ConvertAll(x=>x.Names.FirstOrDefault()));
                sw.WriteLine(t);
                sw.WriteLine();
            }
            sw.Close();
        }
    }

    public class ZhiWei
    {
        public string Id;

        public List<string> Names;

        public string ParentId;

        public int Index;
    }
}

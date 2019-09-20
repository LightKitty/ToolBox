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
    public class TTimingTest
    {
        [TestMethod]
        public void Test()
        {
            TTiming timing = new TTiming();
            int[] nums = new int[100000000];
            timing.StartTime();
            BuildArray(nums);
            //DisplayNums(nums);
            timing.StopTime();
            Console.WriteLine("time(.NET):" + timing.Result().TotalSeconds);
        }

        void BuildArray(int[] arr)
        {
            for(int i=0;i<arr.Length;i++)
            {
                arr[i] = i;
            }
        }

        void DisplayNums(int[] arr)
        {
            for(int i=0;i<arr.GetUpperBound(0);i++)
            {
                Console.WriteLine(arr[i] + " ");
            }
        }
    }
}

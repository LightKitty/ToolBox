using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox
{
    /// <summary>
    /// 测定时间(并没有发现有什么卵用)
    /// </summary>
    public class TTiming
    {
        TimeSpan startTime;
        TimeSpan duration;
        public TTiming()
        {
            startTime = new TimeSpan(0);
            duration = new TimeSpan(0);
        }

        public void StopTime()
        {
            duration = Process.GetCurrentProcess().Threads[0].UserProcessorTime.Subtract(startTime);
        }

        public void StartTime()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            startTime = Process.GetCurrentProcess().Threads[0].UserProcessorTime;
        }

        public TimeSpan Result()
        {
            return duration;
        }
    }
}

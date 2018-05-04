using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace DailyRemindPlus
{
    /// <summary>
    /// 日报提醒
    /// </summary>
    public class DailyRemindJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var service = new DailyRemindService();
            service.Check();
        }
    }
}

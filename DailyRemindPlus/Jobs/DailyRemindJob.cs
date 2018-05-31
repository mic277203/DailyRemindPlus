using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Quartz;

namespace DailyRemindPlus
{
    /// <summary>
    /// 日报提醒
    /// </summary>
    public class DailyRemindJob : IJob
    {
        private static ILog _log = LogManager.GetLogger(typeof(DailyRemindJob));
        public void Execute(IJobExecutionContext context)
        {
            _log.Info("日报检查开始执行");
            var service = new DailyRemindService();
            try
            {
                service.Check();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
    }
}

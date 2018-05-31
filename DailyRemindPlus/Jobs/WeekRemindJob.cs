using log4net;
using Quartz;
using System;

namespace DailyRemindPlus
{
    /// <summary>
    /// 周报提醒
    /// </summary>
    public class WeekRemindJob : IJob
    {
        private static ILog _log = LogManager.GetLogger(typeof(WeekRemindJob));
        public void Execute(IJobExecutionContext context)
        {
            _log.Info("周报检查开始执行");
            var service = new WeekRemindService();
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

using Quartz;

namespace DailyRemindPlus
{
    /// <summary>
    /// 周报提醒
    /// </summary>
    public class WeekRemindJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var service = new WeekRemindService();
            service.Check();
        }
    }
}

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
    public class RemindJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var loginResult = EIPSys.Login(new EIPSys.LoginModel()
            {
                UserName = "lph",
                Password = "lph"
            });

            if (loginResult)
            {
                EIPSys.Check();
            }
        }
    }
}

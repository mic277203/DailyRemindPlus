using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace DailyRemindPlus
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        private static IScheduler _scheduler;
        protected override void OnStart(string[] args)
        {
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();

            IJobDetail jobDailyRemind = JobBuilder.Create<RemindJob>()
               .WithIdentity("jobDailyRemind", "Remind")
               .Build();

            ITrigger tgDailyRemind = TriggerBuilder.Create()
                                    .WithIdentity("tgDailyRemind", "Remind")
                                    .StartNow()
                                    .WithCronSchedule("0 0 18 * * ?")
                                    .Build();

            _scheduler.ScheduleJob(jobDailyRemind, tgDailyRemind);

            _scheduler.Start();
        }

        protected override void OnStop()
        {
        }
    }
}

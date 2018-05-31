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
using Microsoft.Owin.Hosting;
using Owin;
using CrystalQuartz.Owin;
using log4net;

namespace DailyRemindPlus
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        private static IScheduler _scheduler;
        private static ILog _log = LogManager.GetLogger(typeof(Service1));
        protected override void OnStart(string[] args)
        {
            try
            {
                _scheduler = StdSchedulerFactory.GetDefaultScheduler();
                //日报
                IJobDetail jobDailyRemind = JobBuilder.Create<DailyRemindJob>()
                   .WithIdentity("jobDailyRemind", "Remind")
                   .Build();

                ITrigger tgDailyRemind = TriggerBuilder.Create()
                                        .WithIdentity("tgDailyRemind", "Remind")
                                        .StartNow()
                                        .WithCronSchedule("0 0 18 * * ?")
                                        .Build();

                _scheduler.ScheduleJob(jobDailyRemind, tgDailyRemind);

                //周报
                IJobDetail jobWeekRemind = JobBuilder.Create<WeekRemindJob>()
                  .WithIdentity("jobWeekRemind", "Remind")
                  .Build();

                ITrigger tgWeekRemind = TriggerBuilder.Create()
                                        .WithIdentity("tgWeekRemind", "Remind")
                                        .StartNow()
                                        .WithCronSchedule("0 0 18 ? * FRI")
                                        .Build();

                _scheduler.ScheduleJob(jobWeekRemind, tgWeekRemind);

                Action<IAppBuilder> startup = app =>
                {
                    app.UseCrystalQuartz(_scheduler);
                };

                WebApp.Start("http://localhost:9876/", startup);
                _scheduler.Start();

                _log.Info("服务启动完成");
            }
            catch (Exception e)
            {
                _log.Info(e);
            }
        }

        protected override void OnStop()
        {
            _log.Info("服务停止");
        }
    }
}

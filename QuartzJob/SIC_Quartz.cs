using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sephiroth.Infrastructure.Common.QuartzJob
{
    /// <summary>
    /// 
    /// </summary>
    public class SIC_Quartz
    {
        /// <summary>
        /// 任务工厂
        /// </summary>
        /// <typeparam name="T">工作类</typeparam>
        /// <param name="DetailName">工作名称</param>
        /// <param name="TriggerName">触发器名称</param>
        /// <param name="Minute">多长时间出发一次</param>
        public static async Task JobsFactoryAsync<T>(string DetailName, string TriggerName, int Minute)
            where T : IJob
        {
            //工厂1
            ISchedulerFactory factory = new StdSchedulerFactory();
            //启动
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();
            //描述工作
            IJobDetail jobDetail = new JobDetailImpl(DetailName, null, typeof(T));
            //触发器
            ISimpleTrigger trigger = new SimpleTriggerImpl(TriggerName,
                null,
                DateTime.Now,
                null,
                SimpleTriggerImpl.RepeatIndefinitely,
                TimeSpan.FromSeconds(Minute));
            //执行
            await scheduler.ScheduleJob(jobDetail, trigger);
        }

        /// <summary>
        /// 时间间隔执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="seconds">时间间隔(单位：毫秒)</param>
        public static async Task JobsFactoryAsync<T>(int seconds) where T : IJob
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            //IJobDetail job = JobBuilder.Create<T>().WithIdentity("job1", "group1").Build();
            IJobDetail job = JobBuilder.Create<T>().Build();

            ITrigger trigger = TriggerBuilder.Create()
.StartNow()
.WithSimpleSchedule(x => x.WithIntervalInSeconds(seconds).RepeatForever())
.Build();

            await scheduler.ScheduleJob(job, trigger);

            await scheduler.Start();
        }

        /// <summary>
        /// 指定时间执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="cronExpression">cron表达式，即指定时间点的表达式</param>
        public static async Task JobsFactoryAsync<T>(string cronExpression) where T : IJob
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            IJobDetail job = JobBuilder.Create<T>().Build();

            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
.WithCronSchedule(cronExpression)
.Build();

            await scheduler.ScheduleJob(job, trigger);

            await scheduler.Start();

        }
     }
}


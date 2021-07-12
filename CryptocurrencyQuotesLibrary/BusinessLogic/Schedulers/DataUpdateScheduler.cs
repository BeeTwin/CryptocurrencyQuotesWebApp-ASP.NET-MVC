using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using CryptocurrencyQuotesLibrary.BusinessLogic.Constants;

namespace CryptocurrencyQuotesLibrary.BusinessLogic.Schedulers
{
    public class DataUpdateScheduler
    {
        public static async void Start(IServiceProvider serviceProvider)
        {
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
            await scheduler.Start();

            var jobDetail = JobBuilder.Create<DataUpdateJob>().Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity("DataUpdateTrigger", "default")
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(TimeConstants.MINUTES_PER_UPDATE)
                .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}

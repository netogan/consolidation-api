using Consolidation.Api.CronJob.Jobs;
using Quartz;

namespace Consolidation.Api.CronJob.Extension
{
    public static class CronJobExtension
    {
        public static IApplicationBuilder StartCronSchedulingConfig(this IApplicationBuilder builder)
        {
            var scheduler = builder.ApplicationServices
                .GetRequiredService<ISchedulerFactory>().GetScheduler().GetAwaiter();

            var job = JobBuilder.Create<ConsolidationJob>().Build();
            var config = builder.ApplicationServices.GetService<IConfiguration>();

            //Runs every 1 minutes
            var cronExpression = "0 * 0 ? * * *";

            //var trigger = TriggerBuilder.Create()
            //    .WithCronSchedule(cronExpression).Build();

            var trigger = TriggerBuilder.Create()
                .StartAt(DateTimeOffset.Now.AddSeconds(5))
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromMinutes(15))
                    .RepeatForever())
                .Build();

            scheduler.GetResult().ScheduleJob(job, trigger);

            return builder;
        }
    }
}

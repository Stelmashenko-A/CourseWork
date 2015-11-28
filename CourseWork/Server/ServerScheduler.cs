using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Repository;

namespace Server
{
    public class ServerScheduler
    {
        public static void Start()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory=new ServerJobFactory(new Storage());
            scheduler.Start();

            var job = JobBuilder.Create<Server>().Build();

            var trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(15)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }

    public class ServerJobFactory : IJobFactory
    {
        private Storage _storage;

        public ServerJobFactory(Storage storage)
        {
            this._storage = storage;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return new Server(_storage);
        }

        public void ReturnJob(IJob job)
        {
            

        }
    }

}

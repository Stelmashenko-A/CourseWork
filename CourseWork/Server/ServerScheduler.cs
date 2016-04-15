using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Repository;

namespace Server
{
    public class ServerScheduler
    {
        public ServerScheduler(IStorage storage)
        {
            _storage = storage;
        }

        private readonly IStorage _storage;

        public void Start()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = new ServerJobFactory(_storage);
            scheduler.Start();

            var job = JobBuilder.Create<Server>().Build();

            var trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                (s =>
                    s.WithIntervalInMinutes(1)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }

    public class ServerJobFactory : IJobFactory
    {
        private readonly IStorage _storage;

        public ServerJobFactory(IStorage storage)
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

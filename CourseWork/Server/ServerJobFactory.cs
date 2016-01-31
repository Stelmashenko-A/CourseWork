using Quartz;
using Quartz.Spi;
using Repository;

namespace Server
{
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
using System;
using Quartz;
using Repository;

namespace Server
{
    public class Server : IJob
    {
        private readonly Storage _storage = new Storage();
        public void Execute(IJobExecutionContext context)
        {
            //_accountRepository.
            Console.Beep();
        }
    }
}
